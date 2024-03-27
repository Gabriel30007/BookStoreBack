using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Completions;
using Shop.DAL.AppDbContexts;
using ShopBLL.Managers.IManagers;
using ShopBLL.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBLL.Managers
{
    public class BookSearchAI : IBookSearchAI
    {
        private readonly ShopContext _db;
        public BookSearchAI(ShopContext db)
        {
            _db = db;
        }

        public async Task FindBookAsync(string promtUser,string apiKey)
        {
            var openAiApiKey = apiKey;

            APIAuthentication aPIAuthentication = new APIAuthentication(openAiApiKey);
            OpenAIAPI openAiApi = new OpenAIAPI(aPIAuthentication);

            try
            {
                promtUser = "My third and first book ";
                string prompt = "I have such object. Please Give me the fittest book by this promt :" + promtUser;
                string model = "davinci-002";//
                int maxTokens = 50;

                var completionRequest = new CompletionRequest
                {
                    Prompt = prompt,
                    Model = model,
                    MaxTokens = maxTokens
                };

                var conversation = openAiApi.Chat.CreateConversation();
                var data = await _db.Product.Join(
            _db.Author,
            p => p.authorID,
            a => a.Id,
            (p, a) => new
            {
                ID = p.ID,
                Name = p.Name,
                Description = p.Description,
                GenreID = p.genreID,
                AuthorID = p.authorID,
                AuthorName = a.Name

            }).Join(_db.Genre, o => o.GenreID,
                                g => g.ID, (o, g) => new
                                {
                                    ID = o.ID,
                                    Name = o.Name,
                                    Description = o.Description,
                                    Genre = g.Name,
                                    AuthorName = o.Name
                                })
                .AsQueryable().ToArrayAsync();

                ;
                string itemsStr = null;
                foreach(var item in data)
                {
                    itemsStr += "{ ID: " + item.ID + ", Name: " + item.Name + ", Description: "+ item.Description + ", Genre: " + item.Genre + ", Author" + item.AuthorName + " }";
                }

                conversation.AppendUserInput("I have such array of objects." + itemsStr + 
                    ". Please answer me only ID of the fittest book by this promt: " + promtUser + 
                    ". without any words. You can return more than 1 ID if they fit") ;
                var response = await conversation.GetResponseFromChatbotAsync();

                Guid[] productIds = response
                    .Split("\n")
                    .Select(guidString => Guid.Parse(guidString.Trim()))
                    .ToArray();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
