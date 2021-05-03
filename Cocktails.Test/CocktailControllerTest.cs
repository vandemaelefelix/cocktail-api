using System.Text;
using System.Net;
using System.Net.Http;
using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Cocktails.API.DTO;
using Cocktails.API.Models;
using System.IO;

namespace Cocktails.Test
{
    public class CocktailControllerTest: IClassFixture<CustomApiWebApplicationFactory>
    {
        public HttpClient Client { get; set; }

        public CocktailControllerTest(CustomApiWebApplicationFactory fixture)
        {
            Client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_Categories_Should_Return_Ok()
        {
            var response = await Client.GetAsync("/api/categories");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(await response.Content.ReadAsStringAsync());
            Assert.True(categories.Count > 0);
        }

        [Fact]
        public async Task Get_Category_By_Id_Should_Return_Ok()
        {
            var response = await Client.GetAsync("/api/categories/1");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var category = JsonConvert.DeserializeObject<CategoryDTO>(await response.Content.ReadAsStringAsync());
            Assert.IsType<CategoryDTO>(category);
        }


        [Fact]
        public async Task Get_Ingredients_Should_Return_Ok()
        {
            var response = await Client.GetAsync("/api/ingredients");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var categories = JsonConvert.DeserializeObject<List<IngredientDTO>>(await response.Content.ReadAsStringAsync());
            Assert.True(categories.Count > 0);
        }

        [Fact]
        public async Task Get_Ingredient_By_Id_Should_Return_Ok()
        {
            var response = await Client.GetAsync("/api/ingredients/bfe516d6-eee3-4b18-02f2-08d90c8ec70c");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var ingredient = JsonConvert.DeserializeObject<IngredientDTO>(await response.Content.ReadAsStringAsync());
            Assert.IsType<IngredientDTO>(ingredient);
        }

        [Fact]
        public async Task Get_Cocktails_Should_Return_Ok()
        {
            var response = await Client.GetAsync("/api/cocktails");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var categories = JsonConvert.DeserializeObject<List<Cocktail>>(await response.Content.ReadAsStringAsync());
            Assert.True(categories.Count > 0);
        }

        [Fact]
        public async Task Get_Cocktail_By_Id_Should_Return_Ok()
        {
            var response = await Client.GetAsync("/api/cocktails/7e3ed420-1664-4a26-2511-08d90cb1a941");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var cocktail = JsonConvert.DeserializeObject<Cocktail>(await response.Content.ReadAsStringAsync());
            Assert.IsType<Cocktail>(cocktail);
        }

        [Fact]
        public async Task Add_Cocktail()
        {
            string base64Image = Convert.ToBase64String(File.ReadAllBytes("C:/src/cocktail-api/Cocktails.Test/TestImages/test-image.png"));

            var cocktail = new CocktailDTO() {
                Name = "Test cocktail",
                Alcoholic = true,
                Description = "Cocktail with Coke and brown rum",
                Categories = new List<int>(),
                Ingredients = new List<Guid>(),
                ImageEncoded = new List<string>(),
                Extension = new List<string> {"jpg"}
            };

            string json = JsonConvert.SerializeObject(cocktail);

            var response = await Client.PostAsync("/api/cocktails", new StringContent(json, Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK); 

            var createdCocktail = JsonConvert.DeserializeObject<CocktailDTO>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(createdCocktail);
            Assert.Equal<string>("Test cocktail", createdCocktail.Name);
        }

        [Fact]
        public async Task Add_Ingredient()
        {
            string base64Image = Convert.ToBase64String(File.ReadAllBytes("C:/src/cocktail-api/Cocktails.Test/TestImages/test-image.png"));

            var ingredient = new AddIngredientDTO() {
                Name = "Test ingredient",
                Description = "Test description",
                AlcoholPercentage = 40,
                EncodedImages = new List<string>(),
                Extensions = new List<string>()
            };

            string json = JsonConvert.SerializeObject(ingredient);

            var response = await Client.PostAsync("/api/ingredients", new StringContent(json, Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK); 

            var createdIngredient = JsonConvert.DeserializeObject<AddIngredientDTO>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(createdIngredient);
            Assert.Equal<string>("Test ingredient", createdIngredient.Name);
        }

        [Fact]
        public async Task Add_Category()
        {
            string base64Image = Convert.ToBase64String(File.ReadAllBytes("C:/src/cocktail-api/Cocktails.Test/TestImages/test-image.png"));

            var category = new AddCategoryDTO() {
                Name = "Test category"
            };

            string json = JsonConvert.SerializeObject(category);

            var response = await Client.PostAsync("/api/categories", new StringContent(json, Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK); 

            var createdCategory = JsonConvert.DeserializeObject<AddCategoryDTO>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(createdCategory);
            Assert.Equal<string>("Test category", createdCategory.Name);
        }

        [Fact]
        public async Task Update_Cocktail()
        {
            var cocktail = new CocktailUpdateDTO() {
                Name = "Update",
                Alcoholic = true,
                Description = "Cocktail with Coke and brown rum"
            };

            string json = JsonConvert.SerializeObject(cocktail);

            var response = await Client.PutAsync("/api/cocktails/7e3ed420-1664-4a26-2511-08d90cb1a941", new StringContent(json, Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK); 

            var updatedCocktail = JsonConvert.DeserializeObject<Cocktail>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(updatedCocktail);
            Assert.Equal<string>("Update", updatedCocktail.Name);
        }

        [Fact]
        public async Task Update_Category()
        {
            var category = new UpdateCategoryDTO() {
                Name = "Update"
            };

            string json = JsonConvert.SerializeObject(category);

            var response = await Client.PutAsync("/api/categories/1", new StringContent(json, Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK); 

            var updatedCategory = JsonConvert.DeserializeObject<Category>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(updatedCategory);
            Assert.Equal<string>("Update", updatedCategory.Name);
        }

        [Fact]
        public async Task Update_Ingredient()
        {
            var ingredient = new UpdateIngredientDTO() {
                Name = "Update",
                AlcoholPercentage = 40,
                Description = "Test Ingredient"
            };

            string json = JsonConvert.SerializeObject(ingredient);

            var response = await Client.PutAsync("/api/ingredients/bfe516d6-eee3-4b18-02f2-08d90c8ec70c", new StringContent(json, Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK); 

            var updatedIngredient = JsonConvert.DeserializeObject<Ingredient>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(updatedIngredient);
            Assert.Equal<string>("Update", updatedIngredient.Name);
        }
        
        [Fact]
        public async Task Delete_Cocktail()
        {
            var response = await Client.DeleteAsync("/api/cocktails/16ecd799-80fb-49c6-1081-08d90d8408c1");
            response.StatusCode.Should().Be(HttpStatusCode.OK); 

            var deletedCocktail = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(deletedCocktail);
            Assert.IsType<Guid>(deletedCocktail);
        }


    }
}
