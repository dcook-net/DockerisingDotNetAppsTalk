using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FrontEnd.Models;
using MeetupMembersApi.Tests;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FrontEnd
{
    public class MembersApiService
    {
        private readonly HttpClient _httpClient;

        public MembersApiService(HttpClient httpClient, IOptions<MembersApiConfiguration> apiConfiguration)
        {
            httpClient.BaseAddress = apiConfiguration.Value.Uri;
            httpClient.Timeout = TimeSpan.FromMilliseconds(apiConfiguration.Value.Timeout.GetValueOrDefault(2000));
            _httpClient = httpClient;
        }

        public async Task<List<Member>> GetMembersAsync()
        {
            var response = await _httpClient.GetAsync("members");

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<Member>>(await response.Content.ReadAsStringAsync(), new JsonSerializerSettings());
            
            return new List<Member>();
        }

        public async Task<Member> GetMember(string id)
        {
            var response = await _httpClient.GetAsync($"members/{id}");
            
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Member>(await response.Content.ReadAsStringAsync(), new JsonSerializerSettings());

            return new Member();
        }

        public async Task<Member> UpdateMemberDetails(Member member)
        {
            var _ = await _httpClient.PutAsync("members", new JsonContent(member));

            return member;
        }

        public async Task<bool> DeleteMember(string id)
        {
            var _ = await _httpClient.DeleteAsync($"members/{id}");

            return true;
        }

        public async Task<bool> CreateMember(Member newMember)
        {
            var _ = await _httpClient.PostAsync("members", new JsonContent(newMember));

            return true;
        }
    }
}