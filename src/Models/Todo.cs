﻿namespace TodoAppMicroservice.Models
{
    public class Todo
    {
        public string id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public bool isComplete { get; set; }
    }
}
