using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models;


    public class OpenAISettings
    {
        public string ApiKey { get; set; }
        public string ModelName { get; set; } = "gpt-3.5-turbo";
        public string Path { get; set; } = "https://api.openai.com/v1/chat/completions";
}

