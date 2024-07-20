using DotNet8JsonCrud.Api.Enums;
using DotNet8JsonCrud.Api.Helpers;
using DotNet8JsonCrud.Api.Models;
using DotNet8JsonCrud.Api.Resources;

namespace DotNet8JsonCrud.Api.Features.Blog
{
    public class DA_Blog
    {
        private readonly JsonFileHelper _jsonFileHelper;

        public DA_Blog(JsonFileHelper jsonFileHelper)
        {
            _jsonFileHelper = jsonFileHelper;
        }

        #region Get Blogs

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

        #endregion

        #region Create Blog

        public async Task<Result<BlogResponseModel>> CreateBlog(BlogModel blog)
        {
            Result<BlogResponseModel> responseModel;
            try
            {
                blog.BlogId = GetUlid();
                var lst = await _jsonFileHelper.GetJsonData<BlogModel>();
                lst.Add(blog);

                await _jsonFileHelper.WriteJsonDataV1(lst);

                responseModel = Result<BlogResponseModel>.SaveSuccessResult();
            }
            catch (Exception ex)
            {
                responseModel = Result<BlogResponseModel>.FailureResult(ex);
            }

            return responseModel;
        }

        #endregion

        #region Update Blog

        public async Task<Result<BlogResponseModel>> UpdateBlog(BlogModel blog, string blogId)
        {
            Result<BlogResponseModel> responseModel;
            try
            {
                var lst = await _jsonFileHelper.GetJsonData<BlogModel>();
                var item = lst.FirstOrDefault(x => x.BlogId == blogId);

                if (item is null)
                {
                    responseModel = Result<BlogResponseModel>.NotFoundResult();
                    goto result;
                }

                #region Update

                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;

                #endregion

                await _jsonFileHelper.WriteJsonDataV1(lst);

                responseModel = Result<BlogResponseModel>.UpdateSuccessResult();
            }
            catch (Exception ex)
            {
                responseModel = Result<BlogResponseModel>.FailureResult(ex);
            }

        result:
            return responseModel;
        }

        #endregion

        #region Patch Blog

        public async Task<Result<BlogResponseModel>> PatchBlog(BlogModel blog, string blogId)
        {
            Result<BlogResponseModel> responseModel;
            try
            {
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

                #region Patch Method Validation

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

                #endregion

                await _jsonFileHelper.WriteJsonDataV1(lst);

                responseModel = Result<BlogResponseModel>.UpdateSuccessResult();
            }
            catch (Exception ex)
            {
                responseModel = Result<BlogResponseModel>.FailureResult(ex);
            }

        result:
            return responseModel;
        }

        #endregion

        #region Delete Blog

        public async Task<Result<BlogResponseModel>> DeleteBlog(string blogId)
        {
            Result<BlogResponseModel> responseModel;
            try
            {
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

                var lstToInsert = lst.Where(x => x.BlogId != item.BlogId).ToList();
                await _jsonFileHelper.WriteJsonDataV1(lstToInsert);

                responseModel = Result<BlogResponseModel>.SuccessResult(
                    MessageResource.DeleteSuccess,
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

        #endregion

        private string GetUlid() => DevCode.GetUlid();
    }
}