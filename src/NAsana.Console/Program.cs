namespace NAsana
{
    using System;
    using API.v1;
    using API.v1.Utils;

    public class Program
    {
        public static void Main(string[] arguments)
        {
            var asanaConfig = new AsanaConfigManager().GetConfig();

            var client = new AsanaClient(asanaConfig);
            Console.WriteLine("Authorization successful");

            var workspaces = client.Workspace.GetWorkspaces();
            foreach (var workspace in workspaces)
            {
                Console.WriteLine("Workspace:{0}", workspace.Name);
            }

            Console.ReadLine();
        }
    }
}