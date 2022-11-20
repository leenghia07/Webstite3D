﻿namespace WebApp.Models.ViewModel
{
    public class VMArticle
    {
        public Article? Article { get; set; }
        public IEnumerable<Article>? Articles { get; set; }
        public IEnumerable<TypeOfArticle> TypeOfArticles { get; set; }
        public IEnumerable<ExhibitionRoom> ExhibitionRooms { get; set; }
        public IEnumerable<Artifact> Artifacts { get; set; }
        public string? Type { get; set; }
    }
}