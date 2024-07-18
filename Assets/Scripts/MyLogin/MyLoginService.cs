using RMC.Mini.Service;
using System.Threading.Tasks;

public class MyLoginService : BaseService
{
    public void Login(string username, string password)
    {
        // simulate a login with a delay
        Task.Delay(2000).ContinueWith((task) =>
        {
            if (username == "admin" && password == "admin")
            {
                Context.CommandManager.InvokeCommand(new MyLoginCommands.LoginResponseCommand(true, $"{{\"u\":\"{username}\",\"p\":\"{password}\"}}"));
            }
            else
            {
                Context.CommandManager.InvokeCommand(new MyLoginCommands.LoginResponseCommand(false, "Invalid username or password"));
            }
        });
    }
}
