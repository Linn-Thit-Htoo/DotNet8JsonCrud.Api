using DotNet8JsonCrud.Api.Helpers;
using DotNet8JsonCrud.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DotNet8JsonCrud.Api.Features.Blog
{
    public class BL_Blog
    {
        private readonly JsonFileHelper _jsonFileHelper;

        public BL_Blog(JsonFileHelper jsonFileHelper)
        {
            _jsonFileHelper = jsonFileHelper;
        }

        public async Task<Result<BlogListResponseModel>> GetBlogs()
        {
            Result<BlogListResponseModel> responseModel;
            try
            {
                var lst = await _jsonFileHelper.GetJsonData<BlogModel>();
                var model = new BlogListResponseModel(lst);

                responseModel = Result<BlogListResponseModel>.SuccessResult(model);
            }
            catch (Exception ex)
            {
                responseModel = Result<BlogListResponseModel>.FailureResult(ex);
            }

            return responseModel;
        }
    }
}
