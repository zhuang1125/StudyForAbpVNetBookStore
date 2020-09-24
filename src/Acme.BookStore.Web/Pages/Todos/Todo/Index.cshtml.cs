using System.Threading.Tasks;

namespace Acme.BookStore.Web.Pages.Todos.Todo
{
    public class IndexModel : BookStorePageModel
    {
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
