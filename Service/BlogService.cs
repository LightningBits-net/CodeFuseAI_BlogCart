﻿using CodeFuseAI_BlogCart.Service.IService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using CodeFuseAI_Shared.Models;

namespace CodeFuseAI_BlogCart.Service
{
    public class BlogService : IBlogService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration? _configuration;
        private string? BaseServerUrl;

        public BlogService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }


        public async Task<BlogDTO> Get(int blogId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/Api/blog/{blogId}");
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var blog = JsonConvert.DeserializeObject<BlogDTO>(content);
                    blog.ImageUrl = BaseServerUrl + blog.ImageUrl;
                    return blog;
                }
                else
                {
                    var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                    throw new Exception(errorModel.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting the blog: {ex.Message}");
                return new BlogDTO();
            }
        }

        public async Task<IEnumerable<BlogDTO>> GetAll()
        {
            try
            {
                var response = await _httpClient.GetAsync("/Api/Blog");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var blogs = JsonConvert.DeserializeObject<IEnumerable<BlogDTO>>(content);
                    foreach (var blog in blogs)
                    {
                        blog.ImageUrl = BaseServerUrl + blog.ImageUrl;
                    }
                    return blogs;
                }
                else
                {
                    return Enumerable.Empty<BlogDTO>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting all blogs: {ex.Message}");
                return Enumerable.Empty<BlogDTO>();
            }
        }

        public async Task<BlogDTO> Update(int blogId, int rating, int views)
        {
            try
            {
                var updatedData = new
                {
                    Rating = rating,
                };

                var response = await _httpClient.PutAsJsonAsync($"/Api/blog/{blogId}", updatedData);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var updatedBlog = JsonConvert.DeserializeObject<BlogDTO>(content);
                return updatedBlog;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the blog: {ex.Message}");
                return new BlogDTO();
            }
        }

        public async Task<IEnumerable<BlogDTO>> GetBlogsByDomain(string domain)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/Api/Blog/GetBlogsByDomain/{domain}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var blogs = JsonConvert.DeserializeObject<IEnumerable<BlogDTO>>(content);

                    // Update the image URLs with the base server URL
                    foreach (var blog in blogs)
                    {
                        blog.ImageUrl = BaseServerUrl + blog.ImageUrl;
                    }

                    return blogs;
                }
                else
                {
                    return Enumerable.Empty<BlogDTO>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting blogs by domain: {ex.Message}");
                return Enumerable.Empty<BlogDTO>();
            }
        }
    }
}


