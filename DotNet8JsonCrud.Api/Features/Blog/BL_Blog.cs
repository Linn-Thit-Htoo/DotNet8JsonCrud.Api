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
                var lst = await _jsonFileHelper.GetJsonData<BlogModel>();
                lst.Add(blog);

                await _jsonFileHelper.WriteJsonDataV1(lst);

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

        public async Task<Result<BlogResponseModel>> UpdateBlog(BlogModel blog, string blogId)
        {
            Result<BlogResponseModel> responseModel;
            try
            {
                if (string.IsNullOrEmpty(blogId))
                {
                    responseModel = Result<BlogResponseModel>.FailureResult(MessageResource.InvalidId);
                    goto result;
                }

                var lst = await _jsonFileHelper.GetJsonData<BlogModel>();
                var item = lst.FirstOrDefault(x => x.BlogId == blogId);

                if (item is null)
                {
                    responseModel = Result<BlogResponseModel>.FailureResult(
                        MessageResource.NotFound,
                        EnumStatusCode.NotFound
                    );
                    goto result;
                }

                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;

                await _jsonFileHelper.WriteJsonDataV1(lst);

                responseModel = Result<BlogResponseModel>.SuccessResult(
                    MessageResource.UpdateSuccess,
                    EnumStatusCode.Success
                );
            }
            catch (Exception ex)
            {
                responseModel = Result<BlogResponseModel>.FailureResult(ex);
            }

        result:
            return responseModel;
        }

        public async Task<Result<BlogResponseModel>> PatchBlog(BlogModel blog, string blogId)
        {
            Result<BlogResponseModel> responseModel;
            try
            {
                if (string.IsNullOrEmpty(blogId))
                {
                    responseModel = Result<BlogResponseModel>.FailureResult(MessageResource.InvalidId);
                    goto result;
                }

                var lst = await _jsonFileHelper.GetJsonData<BlogModel>();
                var item = lst.FirstOrDefault(x => x.BlogId == blogId);

                if (item is null)
                {
                    responseModel = Result<BlogResponseModel>.FailureResult(
                        MessageResource.NotFound,
                        EnumStatusCode.NotFound
                    );
                    goto result;
                }

                if (!string.IsNullOrEmpty(blog.BlogTitle))
                {
                    item.BlogTitle = blog.BlogTitle;
                }

                if (!string.IsNullOrEmpty(blog.BlogAuthor))
                {
                    item.BlogAuthor = blog.BlogAuthor;
                }

                if (!string.IsNullOrEmpty(blog.BlogContent))
                {
                    item.BlogContent = blog.BlogContent;
                }

                await _jsonFileHelper.WriteJsonDataV1(lst);

                responseModel = Result<BlogResponseModel>.SuccessResult(
                    MessageResource.UpdateSuccess,
                    EnumStatusCode.Success
                );
            }
            catch (Exception ex)
            {
                responseModel = Result<BlogResponseModel>.FailureResult(ex);
            }

        result:
            return responseModel;
        }
    }
}
