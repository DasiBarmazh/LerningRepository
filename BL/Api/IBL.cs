namespace BL.Api;

public interface IBL
{
    public IBLUser BLUser { get; }
    public IBLCategory BLCategory { get; }
    public IBLSubCategory BLSubCategory { get; }
    public IOpenAI OpenAI { get; }
    public IBLPrompt prompt { get; }


}
