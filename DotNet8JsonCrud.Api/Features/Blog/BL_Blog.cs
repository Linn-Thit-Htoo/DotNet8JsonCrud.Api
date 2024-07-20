namespace DotNet8JsonCrud.Api.Features.Blog;

public class BL_Blog
{
    private readonly JsonFileHelper _jsonFileHelper;
    private readonly DA_Blog _dA_Blog;

    public BL_Blog(JsonFileHelper jsonFileHelper, DA_Blog dA_Blog)
    {
        _jsonFileHelper = jsonFileHelper;
        _dA_Blog = dA_Blog;
    }

    #region Get Blogs

    public async Task<Result<BlogListResponseModel>> GetBlogs()
    {
        return await _dA_Blog.GetBlogs();
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

    #region Create Blog V1

    public async Task<Result<BlogResponseModel>> CreateBlogV1(BlogModel blog)
    {
        Result<BlogResponseModel> responseModel;
        try
        {
            var result = blog.IsValid();
            if (result.IsError)
            {
                responseModel = result;
                goto result;
            }

            responseModel = await _dA_Blog.CreateBlog(blog);
        }
        catch (Exception ex)
        {
            responseModel = Result<BlogResponseModel>.FailureResult(ex);
        }

    result:
        return responseModel;
    }

    #endregion

    #region Update Blog

    public async Task<Result<BlogResponseModel>> UpdateBlog(BlogModel blog, string blogId)
    {
        Result<BlogResponseModel> responseModel;
        try
        {
            if (blogId.IsNullOrEmpty())
            {
                responseModel = GetInvalidIdResult();
                goto result;
            }

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

    #region Update Blog V1

    public async Task<Result<BlogResponseModel>> UpdateBlogV1(BlogModel blog, string blogId)
    {
        Result<BlogResponseModel> responseModel;
        try
        {
            if (blogId.IsNullOrEmpty())
            {
                responseModel = GetInvalidIdResult();
                goto result;
            }

            responseModel = await _dA_Blog.UpdateBlog(blog, blogId);
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
            if (string.IsNullOrEmpty(blogId))
            {
                responseModel = GetInvalidIdResult();
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

    #region Patch Blog V1

    public async Task<Result<BlogResponseModel>> PatchBlogV1(BlogModel blog, string blogId)
    {
        Result<BlogResponseModel> responseModel;
        try
        {
            if (string.IsNullOrEmpty(blogId))
            {
                responseModel = GetInvalidIdResult();
                goto result;
            }

            responseModel = await _dA_Blog.PatchBlog(blog, blogId);
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
            if (string.IsNullOrEmpty(blogId))
            {
                responseModel = GetInvalidIdResult();
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

    #region Delete Blog V1

    public async Task<Result<BlogResponseModel>> DeleteBlogV1(string blogId)
    {
        Result<BlogResponseModel> responseModel;
        try
        {
            if (string.IsNullOrEmpty(blogId))
            {
                responseModel = GetInvalidIdResult();
                goto result;
            }

            responseModel = await _dA_Blog.DeleteBlog(blogId);
        }
        catch (Exception ex)
        {
            responseModel = Result<BlogResponseModel>.FailureResult(ex);
        }

    result:
        return responseModel;
    }

    #endregion

    private Result<BlogResponseModel> GetInvalidIdResult() =>
        Result<BlogResponseModel>.FailureResult(MessageResource.InvalidId);

    private string GetUlid() => DevCode.GetUlid();
}
