using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;
using Postogram.Common;
using Postogram.Common.Logger;

namespace Postogram.InstagramClient.Poster
{
    public class InstaPoster : IPoster
    { 
        private IInstaApi _api;

        private readonly ILogWriter _logger;
        private readonly UserSessionData _userData;
        private readonly IFilePathHelper _fileHelper;
        private readonly IHttpClientPool _httpClientPool;
        private readonly InstagramConfiguration _configuration;

        public bool IsAuthenticated => _api?.IsUserAuthenticated ?? false;

        public InstaPoster(InstagramConfiguration configuration,
            IFilePathHelper fileHelper,
            ILogger logger,
            IHttpClientPool httpClientPool)
        {
            _fileHelper = fileHelper;
            _httpClientPool = httpClientPool;
            _configuration = configuration;

            _logger = logger.CreateWriter<InstaPoster>();
            _userData = new UserSessionData();
        }

        public Task<PostResult> Post(Content content, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task PostAsync(Content content)
        {
            await Authorize();

            if (!IsAuthenticated)
            {
                throw new NotImplementedException();
            }

            await Upload(content);
        }

        private async Task Upload(Content content)
        {
            if (content.Pictures.Length > 1)
            {
                await UploadAlbum(content);
            }
            else
            {
                await UploadSingle(content);
            }
        }

        private async Task UploadSingle(Content content)
        {
            var picture = content.Pictures[0];
            var image = FromPicture(picture);

            var result = await _api.UploadPhotoAsync(image, content.Description);
            if (!result.Succeeded)
            {
                throw new NotImplementedException();
            }
        }

        private async Task UploadAlbum(Content content)
        {
            var images = content.Pictures
                .Select(pic => FromPicture(pic))
                .ToArray();

            var result = await _api.UploadPhotosAlbumAsync(images, content.Description);

            if (!result.Succeeded)
            {
                
            }
        }

        private InstaImage FromPicture(Picture pic)
        {
            throw new NotImplementedException();
        }


        private async Task Authorize()
        {
            if (IsAuthenticated)
            {
                return;
            }

            try
            {
                LoadState();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Init from file error");
            }

            if (IsAuthenticated)
            {
                return;
            }

            _api = InstaApiBuilder.CreateBuilder()
                .SetUser(_userData)
                .UseLogger(GetLogger())
                .UseHttpClient(_httpClientPool.GetHttpClient(nameof(InstaPoster)))
                .Build();

            await _api.LoginAsync();
        }

        private void LoadState()
        {
            var filePath = _fileHelper.GetFile(Location.Application, nameof(InstaPoster), "state.bin");

            using (var stateFileStream = File.Open(filePath, FileMode.OpenOrCreate))
            {
                _api.LoadStateDataFromStream(stateFileStream);
            }
        }

        private InstagramLoggerAdapter GetLogger() => new InstagramLoggerAdapter(_logger, _configuration.ToLogRequests);
    }
}
