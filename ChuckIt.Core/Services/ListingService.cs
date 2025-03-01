﻿using Amazon.Runtime.SharedInterfaces;
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

        public async Task<ListingDto> CreateListingAsync(ListingDto request)
        {
            var listing = new Listing
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                CategoryId = request.CategoryId,
                Price = request.Price,
                Images = new List<Images>()
            };

            foreach(var base64Image in request.ImageFileName)
            {
                string fileExtension = GetImageExtension(base64Image);
                var fileName = $"{Guid.NewGuid()}.{fileExtension}";

                using (var imageStream = GetImageStream(base64Image))
                {
                    var s3Url = await UploadImageToS3Async(imageStream, fileName);
                    listing.Images.Add(new Images { FileName = s3Url });
                }
            }

            await _listingRepository.Create(listing);

            return new ListingDto() { };
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

        public async Task<ListingDto> GetListingDetailsAsync(Guid id)
        {
            var listing = await _listingRepository.GetListingDetailsAsync(id);

            return listing;
        }
    }
}
