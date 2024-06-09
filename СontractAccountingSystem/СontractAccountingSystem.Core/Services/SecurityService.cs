using Salazki.Security;
using Salazki.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Services
{
    internal class SecurityService : ISecurityService
    {
        private readonly Dictionary<string, Role> _roles = new();
        public static string UserRole { get; set; }

        public SecurityService()
        {
            _roles[SecurityRoles.Adminisrator] = new Role(SecurityRoles.Adminisrator, (_, _) => true);
            _roles[SecurityRoles.Director] = new Role(SecurityRoles.Director, (_, _) => true);

        }

        private bool IsActionGrantedForOperator(string action, object? subject)
        {
            if (action != SecurityActions.DeleteTechnology)
                return false;
            var technology = subject as ArchiveDocumentModel;
            return technology != null;
        }

        public Task<ISecurityContext> OpenContext()
        {
            return Task.FromResult<ISecurityContext>(new SecurityContext(this));
        }

        internal Role GetRole(string role)
        {
            return _roles.GetOrDefault(role);
        }
    }

    internal class Role
    {
        private readonly Func<string, object?, bool> _isActionGranted;

        public Role(string name)
        {
            _isActionGranted = (action, subject) => false;
        }

        public Role(string name, Func<string, object?, bool> isActionGranted)
        {
            _isActionGranted = isActionGranted;
        }

        public string Name { get; }
        public bool IsActionGranted(string action, object? subject)
        {
            if (action == SystemSecurityActions.StartApplication)
                return true;
            return _isActionGranted(action, subject);
        }
    }

    internal class User : IUser
    {
        public static IUser[] AllUsers = new IUser[]
        {
            new User("admin", "Администратор"),
        };

        private User(string login, string name)
        {
            Login = login;
            DisplayName = name;
            Description = null;
            Profiles = new IUserProfile[] { new UserProfile(this) };
        }

        public string Login { get; }

        public string DisplayName { get; }

        public string Description { get; }

        public IReadOnlyList<IUserProfile> Profiles { get; }
    }

    internal class UserProfile : IUserProfile
    {
        public UserProfile(User user)
        {
            Owner = user;
        }

        public object Id => Owner.Login;

        public IUser Owner { get; }

        public IUser InsteadOfUser => null;

        public string Name => Owner.DisplayName;

        public string Description => Owner.Description;
    }

    internal class SecurityContext : ISecurityContext
    {
        private readonly SecurityService _securityService;
        private readonly Role _role;

        public SecurityContext(SecurityService securityService)
        {
            _securityService = securityService;
            Owner = User.AllUsers.FirstOrDefault(x => x.Login == SecurityService.UserRole);
            Profile = Owner?.Profiles.FirstOrDefault();
            _role = _securityService.GetRole(SecurityRoles.Operator);
            if (SecurityService.UserRole == "admin")
                _role = _securityService.GetRole(SecurityRoles.Adminisrator);
            if (SecurityService.UserRole == "director")
                _role = _securityService.GetRole(SecurityRoles.Director);
        }

        public IUser Owner { get; }

        public IUserProfile Profile { get; }

        public event EventHandler ProfileChanged;

        public Task ChangeProfile(IUserProfile profile, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public bool IsActionGranted(string actionName, object subject = null)
        {
            if (_role == null)
                return false;
            return _role.IsActionGranted(actionName, subject);
        }

        public bool IsUserInRole(string roleName)
        {
            if (_role == null)
                return false;
            return _role.Name == roleName;
        }
    }

    public static class SecurityActions
    {
        public static readonly string DeleteTechnology = "DeleteTechnology";
    }

    public static class SecurityRoles
    {
        public static readonly string Operator = nameof(Operator);
        public static readonly string Reader = nameof(Reader);
        public static readonly string Adminisrator = "admin";
        public static readonly string Director = "director";

    }
}
