using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        private ILogWriter _logger;

        private readonly FileHelper _fileHelper;
        private readonly UserSessionData _userData;

        private static readonly HttpClient _httpClient = new HttpClient();

        private bool IsAuthenticated => _api?.IsUserAuthenticated ?? false;

        public InstaPoster(InstagramConfiguration configuration, FileHelper fileHelper, ILogger logger)
        {
            _fileHelper = fileHelper;
            _logger = logger.CreateWriter<InstaPoster>();
            _userData = new UserSessionData()
            {
                UserName = configuration.Login,
                Password = configuration.Password
            };
        }

        public void Post(Content content)
        {
            var task = PostAsync(content);
            task.ConfigureAwait(continueOnCapturedContext: false);
            task.GetAwaiter().GetResult();
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
                .UseLogger(new InstaLoggerAdapter(_logger))
                .UseHttpClient(_httpClient)
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

    }
}
