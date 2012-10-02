namespace bbv.Infrastructure.Indexes
{
    using System.Linq;

    using Raven.Client.Indexes;

    using bbv.Models;

    public class Students_ByEmailDomain : AbstractIndexCreationTask<Student, Students_ByEmailDomain.Result>
    {
        public class Result
        {
            public string EmailDomain { get; set; }
            public int Count { get; set; }
        }

        public Students_ByEmailDomain()
        {
            Map = students => from student in students 
                              select new
                                  {
                                      EmailDomain = student.Email.Split('@')[1],
                                      Count = 1
                                  };

            Reduce = results => from result in results
                                group result by result.EmailDomain into g
                                select new
                                    {
                                        EmailDomain = g.Key,
                                        Count = g.Sum(r => r.Count)
                                    };
        }
    }
}