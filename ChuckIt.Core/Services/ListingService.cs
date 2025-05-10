using Amazon.Runtime.SharedInterfaces;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.Util.Internal;
using ChuckIt.Core.Entities.Listings.Dtos;
using ChuckIt.Core.Interfaces.IRepositories;
using ChuckIt.Core.Interfaces.IServices;
using ChuckItApiV2.Core.Entities.Listings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Core.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        
        public ListingService(IListingRepository listingRepository, IAmazonS3 amazonS3)
        {
            _listingRepository = listingRepository;
            _s3Client = amazonS3;
            _bucketName = Environment.GetEnvironmentVariable("S3_BUCKET_NAME") ?? string.Empty;
        }

        public async Task<ListingDto> CreateListingAsync(CreateListingDto request)
        {
            var listing = new Listing(request);

            if (request.ImageFileName != null && request.ImageFileName.Any())
            {
                foreach (var formFile in request.ImageFileName)
                {
                    if (formFile.Length > 0)
                    {
                        var fileExtension = Path.GetExtension(formFile.FileName).TrimStart('.');
                        var fileName = $"{Guid.NewGuid()}.{fileExtension}";

                        using (var stream = formFile.OpenReadStream())
                        {
                            var s3Url = await UploadImageToS3Async(stream, fileName);
                            listing.Images.Add(new Images
                            {
                                Id = Guid.NewGuid(),
                                FileName = s3Url,
                                ListingId = listing.Id
                            });
                        }
                    }
                }
            }

            await _listingRepository.Create(listing);

            return new ListingDto(listing);
        }

        private string GetImageExtension(string base64Image)
        {
            //Checks the first few characters of the base64 string to determine the file type
            if (base64Image.StartsWith("iVBORw0KGgo")) return "png"; // PNG
            if (base64Image.StartsWith("/9j/")) return "jpg"; // JPEG
            if (base64Image.StartsWith("R0lGODdh") || base64Image.StartsWith("R0lGODlh")) return "gif"; // GIF
            if (base64Image.StartsWith("UklGR")) return "webp"; // WEBP

            return "jpg";
        }

        private Stream GetImageStream(string base64Image)
        {
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            return new MemoryStream(imageBytes);
        }

        public async Task<string> UploadImageToS3Async(Stream imageStream, string fileName)
        {
            var fileTransferUtility = new TransferUtility(_s3Client);

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = imageStream,
                Key = fileName,
                BucketName = _bucketName,
                CannedACL = S3CannedACL.PublicRead
            };

            await fileTransferUtility.UploadAsync(uploadRequest);

            string s3Url = $"https://{uploadRequest.BucketName}.s3.amazonaws.com/{fileName}";

            return s3Url;
        }

        public async Task<List<ListingDto>> GetAllListingsAsync()
        {
            return await _listingRepository.GetAllListingsAsync();
        }

        public async Task<Listing> GetListingDetailsAsync(Guid id)
        {
            var listing = await _listingRepository.GetListingDetailsAsync(id);

            return listing;
        }

        public async Task<ListingDto> UpdateListingAsync(UpdateListingDto request, Guid id)
        {
            var listing = await _listingRepository.GetListingDetailsAsync(id);

            listing.Title = request.Title ?? listing.Title;
            listing.CategoryId = request.CategoryId;
            listing.Price = request.Price;

            if (request.ImageFileName != null && request.ImageFileName.Any())
            {
                foreach (var formFile in request.ImageFileName)
                {
                    var fileExtension = Path.GetExtension(formFile.FileName).TrimStart('.');
                    var fileName = $"{Guid.NewGuid()}.{fileExtension}";

                    using (var stream = formFile.OpenReadStream())
                    {
                        var s3Url = await UploadImageToS3Async(stream, fileName);
                        listing.Images.Add(new Images
                        {
                            Id = Guid.NewGuid(),
                            FileName = s3Url,
                            ListingId = listing.Id
                        });
                    }
                }
            }
            await _listingRepository.Update(listing);

            return new ListingDto(listing);
        }

        public async Task DeleteListingAsync(Guid id)
        {
            var listing = await _listingRepository.GetListingDetailsAsync(id);

            if (listing == null)
            {
                throw new Exception($"Listing with ID {id} not found");
            }

            await _listingRepository.Delete(listing);
        }
    }
}
