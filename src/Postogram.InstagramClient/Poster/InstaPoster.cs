using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;
using Postogram.Common;

namespace Postogram.InstagramClient.Poster
{
    public class InstaPoster : IPoster
    {
        private readonly string _login;
        private readonly string _password;

        private IInstaApi _api;
        private bool IsAuthenticated => _api?.IsUserAuthenticated ?? false;

        public InstaPoster(InstagramConfiguration configuration)
        {
            _login = configuration.Login;
            _password = configuration.Password;
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
                throw new NotImplementedException();
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
                //TODO
                // non critical exception at this step
            }

            if (IsAuthenticated)
            {
                return;
            }

            _api = InstaApiBuilder.CreateBuilder()
                .SetUser(new UserSessionData()
                {
                    UserName = _login,
                    Password = _password
                })
                //.UseLogger() TODO: logs
                //.UseHttpClient() TODO: single http client instance
                .Build();

            await _api.LoginAsync();
        }

        private void LoadState()
        {
            var filePath = FileHelper.GetFile(Location.Application, nameof(InstaPoster), "state.bin");

            using (var stateFileStream = File.Open(filePath, FileMode.OpenOrCreate))
            {
                _api.LoadStateDataFromStream(stateFileStream);
            }
        }

    }
}
