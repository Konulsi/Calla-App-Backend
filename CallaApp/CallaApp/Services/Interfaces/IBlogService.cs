using CallaApp.Models;

namespace CallaApp.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int? id);
        Task<BlogImage> GetImageById(int? id);
        void RemoveImage(BlogImage img);
        Task<Blog> GetBlogByImageId(int? id);
        Task<List<Blog>> GetPaginatedDatasAsync(int page, int take);
        Task<int> GetCountAsync();
        Task<List<BlogComment>> GetComments();
        Task<BlogComment> GetCommentById(int? id);
        Task<BlogComment> GetCommentByIdWithBlog(int? id);
        Task<List<Blog>> GetLatestBlogs();

        List<Blog> GetRelatedBlogs();

    }
}
