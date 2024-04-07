namespace Alice1.Models
{


    public class YourModel
    {
        public MetaData meta { get; set; }
        public SessionData session { get; set; }
        public RequestData request { get; set; }
        public StateData state { get; set; }
        public string version { get; set; }
    }

    public class MetaData
    {
        public string locale { get; set; }
        public string timezone { get; set; }
        public string client_id { get; set; }
        public Interfaces interfaces { get; set; }
    }

    public class Interfaces
    {
        public object screen { get; set; }
        public object sayments { get; set; }
        public object account_linking { get; set; }
    }

    public class SessionData
    {
        public int message_id { get; set; }
        public string session_id { get; set; }
        public string skill_id { get; set; }
        public UserData user { get; set; }
        public ApplicationData application { get; set; }
        public string user_id { get; set; }
        public bool New { get; set; }
    }

    public class UserData
    {
        public string user_id { get; set; }
    }

    public class ApplicationData
    {
        public string application_id { get; set; }
    }

    public class RequestData
    {
        public string command { get; set; }
        public string res_command { get; set; }
        public string original_utterance { get; set; }
        public NluData nlu { get; set; }
        public MarkupData markup { get; set; }
        public string type { get; set; }
    }

    public class NluData
    {
        public string[] tokens { get; set; }
        public Entity[] entities { get; set; }
        public object intents { get; set; }
    }

    public class Entity
    {
        public string type { get; set; }
        public Tokens tokens { get; set; }
        public int value { get; set; }
    }

    public class Tokens
    {
        public int start { get; set; }
        public int end { get; set; }
    }

    public class MarkupData
    {
        public bool dangerous_context { get; set; }
    }

    public class StateData
    {
        public object session { get; set; }
        public object user { get; set; }
        public object application { get; set; }
    }


}
