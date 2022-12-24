using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {
        public DevFreelaDbContext(List<Project> projects, List<User> users, List<Skill> skills)
        {
            Projects = new List<Project>()
            {
                new Project("Meu Projeto .Net Core '", "Minha Descrição 1", 1,1 , 10000),
                new Project("Meu Projeto .Net Core 1", "Minha Descrição 2", 1,1 , 20000),
                new Project("Meu Projeto .Net Core 2", "Minha Descrição 2", 1,1 , 30000),
            };
            Users = new List<User>()
            {
                new User("Flavio Pneuy", "flaviopneul@gmail.com", new DateTime(1990,1,1)),
                new User("Luis Pneuy", "flaviopneul@gmail.com", new DateTime(2000,1,1)),
                new User("Hey Pneuy", "flaviopneul@gmail.com", new DateTime(2002,2,2)),
            };

            Skills = new List<Skill>
            {
                new Skill(".NET CORE"),
                new Skill("SQL"),
                new Skill("C#"),
            };
        }

        public List<Project> Projects { get;  set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
        public List<ProjectComment> Comments { get; set; }
    }
}
