using CodeFramework.Controllers;
using GitHubSharp.Models;

namespace CodeHub.Controllers
{
    public class RepositoryInfoController : Controller<RepositoryInfoController.ViewModel>
    {
        public string User { get; private set; }

        public string Repo { get; private set; }

        public RepositoryInfoController(IView<ViewModel> view, string user, string repo)
            : base(view)
        {
            User = user;
            Repo = repo;
        }

        public override void Update(bool force)
        {
            Model = new ViewModel {
                    RepositoryModel = Application.Client.Users[User].Repositories[Repo].Get(force).Data,
                    IsWatched = Application.Client.Users[User].Repositories[Repo].IsWatching(),
                    IsStarred = Application.Client.Users[User].Repositories[Repo].IsStarred()
            };

            try 
            {
                Model.Readme = Application.Client.Users[User].Repositories[Repo].GetReadme(force).Data;
            }
            catch { }
        }

        public void Watch()
        {
            Application.Client.Users[User].Repositories[Repo].Watch();
            Model.IsWatched = true;
        }

        public void StopWatching()
        {
            Application.Client.Users[User].Repositories[Repo].StopWatching();
            Model.IsWatched = false;
        }

        public void Star()
        {
            Application.Client.Users[User].Repositories[Repo].Star();
            Model.IsStarred = true;
        }

        public void Unstar()
        {
            Application.Client.Users[User].Repositories[Repo].Unstar();
            Model.IsStarred = false;
        }

        public class ViewModel
        {
            public RepositoryModel RepositoryModel;
            public bool IsWatched;
            public bool IsStarred;
            public ContentModel Readme;
        }
    }
}

