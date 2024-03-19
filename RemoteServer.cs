using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitSimulation
{
    internal class RemoteServer
    {
        private List<Commit> commitsRemotos;

        public RemoteServer()
        {
            commitsRemotos = new List<Commit>();
        }

        public void ReceiveCommits(List<Commit> commits)
        {
            commitsRemotos.AddRange(commits);
        }
    }
}
