﻿using AspNetMvc_lesson_app.Models.Forms;
using System.Text.Json;

namespace AspNetMvc_lesson_app.Models.Services
{
    public class UserInfoService
    {
        public UserInfoService()
        {
            Load();
        }

        private string userDataFile = "user-info.json";

        private List<UserInfo> UserInfos { get; set; } = [];

        public void Load()
        {
            if (File.Exists(userDataFile))
            {
                UserInfos = JsonSerializer.Deserialize<List<UserInfo>>(File.ReadAllText(userDataFile));
            }
        }

        public List<UserInfo> GetAll()
        {
            return UserInfos;
        }

        public void Add(UserInfo model)
        {
            UserInfos.Add(model);
        }


        public UserInfo? FindById(int id)
        {
            return UserInfos.FirstOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
            File.WriteAllText(userDataFile, JsonSerializer.Serialize(UserInfos));
        }

        //dz
        public void Remove(UserInfo model)
        {
            UserInfos.Remove(model);
        }

        internal void Add(UserInfoForms form)
        {
            throw new NotImplementedException();
        }
    }
}
