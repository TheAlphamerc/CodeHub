using GitHubSharp.Models;
using CodeFramework.Controllers;

namespace CodeHub.Controllers
{
    public class RepositoryStarredController : ListController<BasicUserModel>
    {
        private readonly string _name;
        private readonly string _owner;
        
        public RepositoryStarredController(IListView<BasicUserModel> view, string owner, string name)
            : base(view)
        {
            _name = name;
            _owner = owner;
        }

        public override void Update(bool force)
        {
            var response = Application.Client.Users[_owner].Repositories[_name].GetStargazers(force);
            Model = new ListModel<BasicUserModel> { Data = response.Data, More = this.CreateMore(response) };
        }
    }
}

