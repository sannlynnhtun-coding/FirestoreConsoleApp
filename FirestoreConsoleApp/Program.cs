using FirestoreConsoleApp.Repositories;
using FireStoreConsoleApp.Models;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

var itemInsert = new Blog
{
    Id = Guid.NewGuid().ToString("N"),
    BlogTitle = "C# Firestore",
    BlogAuthor = "SLH",
    BlogContent = ".NET 6"
};
BlogRepository blogRepository = new BlogRepository();
await blogRepository.AddAsync(itemInsert);

var lst = await blogRepository.GetAllAsync();
Console.WriteLine(JsonConvert.SerializeObject(lst, Formatting.Indented));

lst[0].BlogContent = ".NET 6 Console App";
await blogRepository.UpdateAsync(lst[0]);

lst = await blogRepository.GetAllAsync();
Console.WriteLine(JsonConvert.SerializeObject(lst, Formatting.Indented));

await blogRepository.DeleteAsync(lst[0]);
lst = await blogRepository.GetAllAsync();
Console.WriteLine("Final => " + JsonConvert.SerializeObject(lst, Formatting.Indented));

Console.ReadKey();
