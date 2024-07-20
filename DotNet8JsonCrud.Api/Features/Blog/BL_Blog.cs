using DotNet8JsonCrud.Api.Enums;
using DotNet8JsonCrud.Api.Helpers;
using DotNet8JsonCrud.Api.Models;
using DotNet8JsonCrud.Api.Resources;
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

        public async Task<Result<BlogResponseModel>> CreateBlog(BlogModel blog)
        {
            Result<BlogResponseModel> responseModel;
            try
            {
                blog.BlogId = Ulid.NewUlid().ToString();
                await _jsonFileHelper.WriteJsonData(blog);

                responseModel = Result<BlogResponseModel>.SuccessResult(
                    MessageResource.SaveSuccess,
                    EnumStatusCode.Success
                );
            }
            catch (Exception ex)
            {
                responseModel = Result<BlogResponseModel>.FailureResult(ex);
            }

            return responseModel;
        }
    }
}
