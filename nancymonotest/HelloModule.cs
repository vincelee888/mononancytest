using Nancy;

namespace nancymonotest
{
    public class HelloModule : NancyModule
    {
        public HelloModule()
        {
            Get["/"] = parameters => "Hello World!";
        }
    }
}
