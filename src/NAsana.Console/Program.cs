namespace NAsana
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using API.v1;
    using API.v1.Model.Utils;
    using API.v1.Utils;

    public class Program
    {
        private static readonly Dictionary<Type, PropertyInfo[]> TypeCache = new Dictionary<Type, PropertyInfo[]>();

        public static void Main(string[] arguments)
        {
            var asanaConfig = new AsanaConfigManager().GetConfig();
            var activeWorkspaceId = long.Parse(ConfigurationManager.AppSettings["asana.active.workspaceid"]);

            //create and configure asana client
            var client = new AsanaClient(asanaConfig);
            client.ErrorHandler =
                (ex, actionName) =>
                Console.WriteLine("Error occured in {0} method with message: {1}", actionName, ex.Message);

            Console.WriteLine("Authorization successful");

            var workspaces = client.Workspace.GetWorkspaces();
            Console.WriteLine("Workspaces:");
            Console.WriteLine(Print(workspaces));

            var activeWorkspace = workspaces.First(_ => _.Id == activeWorkspaceId);
            var tasks = client.Workspace.GetWorkspaceTasks(activeWorkspaceId, UserPredefinedId.Me);
            Console.WriteLine("Workspace '{0}' tasks:", activeWorkspace.Name);
            Console.WriteLine(Print(tasks));

            Console.ReadLine();
        }

        private static string Print<T>(IEnumerable<T> objs, string lineFormat = "\t{0}") where T : class
        {
            lineFormat = lineFormat + Environment.NewLine;
            var sb = new StringBuilder();
            foreach (var obj in objs)
            {
                sb.AppendFormat(lineFormat, Print(obj));
            }
            return sb.ToString();
        }

        private static string Print(object obj)
        {
            Guard.NotNull("obj", obj);

            PropertyInfo[] properties;
            var type = obj.GetType();
            if (!TypeCache.TryGetValue(type, out properties))
            {
                TypeCache[type] = properties =
                                        type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            }

            return properties.Where(_ => _.CanRead)
                .Aggregate(new StringBuilder(),
                           (sb, prop) =>
                               {
                                   if (sb.Length > 0)
                                   {
                                       sb.Append(",");
                                   }
                                   sb.AppendFormat(@"{0} = {1}", prop.Name, prop.GetValue(obj, null));

                                   return sb;
                               },
                           sb => sb.ToString());
        }
    }
}