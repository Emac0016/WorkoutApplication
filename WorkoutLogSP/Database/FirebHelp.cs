using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace WorkoutLogSP.ViewModels
{
    class FirebHelp
    {

        //This function here sets the connection between the application and the firebase Database for authentication and a realtime DB
        readonly FirebaseClient firebase = new FirebaseClient("https://workoutlogsp-1c6b4.firebaseio.com/");

        //Function to return a list of all the users from the database
        public async Task<List<Users>> GetAllUsers()
        {
            return (await firebase.Child("Users").OnceAsync<Users>()).Select(item => new Users
            {
                Role = item.Object.Role,
                Email = item.Object.Email,
                Name = item.Object.Name,
                ID = item.Object.ID,
                TeamName = item.Object.TeamName,
                Sport = item.Object.Sport,
                TeamCode = item.Object.TeamCode
            }).ToList();
        }

        //This function returns a list of all the teams on the app
        public async Task<List<Team>> GetAllTeams()
        {
            return (await firebase.Child("Teams").OnceAsync<Team>()).Select(item => new Team
            {
                TeamName = item.Object.TeamName,
                TeamCode = item.Object.TeamCode,
                CoachName = item.Object.CoachName,
                Sport = item.Object.Sport
            }).ToList();
        }

        //Function to get all the workouts sent by a coach and from a certain team
        public async Task<List<TeamWorkouts>> GetAllWorkouts(string us)
        {
            return (await firebase.Child("Team Workouts").OnceAsync<TeamWorkouts>()).Select(item => new TeamWorkouts
            {
                UserComp = item.Object.UserComp,
                TimeCreated = item.Object.TimeCreated,
                Type = item.Object.Type,
                Sport = item.Object.Sport,
                Description = item.Object.Description
            }).Where(x => x.UserComp == us).ToList();
        }

        public async Task<List<Workouts>> GetPersonalWorkouts(string user)
        {
            return (await firebase.Child("Personal Workouts").OnceAsync<Workouts>()).Select(item => new Workouts
            {
                UserComp = item.Object.UserComp,
                WorkoutCompleter = item.Object.WorkoutCompleter,
                TimeCreated = item.Object.TimeCreated,
                Type = item.Object.Type,
                Sport = item.Object.Sport,
                Description = item.Object.Description,
                SendWorkout = item.Object.SendWorkout
            }).Where(x => x.UserComp == user).ToList();
        }

        //Function to get all the players under a team roster
        public async Task<List<Users>> GetTeamRoster(string ro, string sp, string te)
        {
            return (await firebase.Child("Users").OnceAsync<Users>()).Select(item => new Users
            {
                Role = item.Object.Role,
                Email = item.Object.Email,
                Name = item.Object.Name,
                ID = item.Object.ID,
                TeamName = item.Object.TeamName,
                Sport = item.Object.Sport,
                TeamCode = item.Object.TeamCode
            }).Where(x => x.Role == ro).Where(y => y.Sport == sp).Where(z => z.TeamName == te).ToList();
        }

        //Function to get the workouts specified by the player
        public async Task<List<Workouts>> GetPlayerWorkouts(string playerName,  string sport, string sendWorkout)
        {
            return (await firebase.Child("Personal Workouts").OnceAsync<Workouts>()).Select(item => new Workouts
            {
                UserComp = item.Object.UserComp,
                WorkoutCompleter = item.Object.WorkoutCompleter,
                TimeCreated = item.Object.TimeCreated,
                Type = item.Object.Type,
                Sport = item.Object.Sport,
                Description = item.Object.Description
            }).Where(x => x.WorkoutCompleter == playerName).Where(x => x.Sport == sport).Where(x => x.SendWorkout == sendWorkout).ToList();
        }

        //Adds a new user to the table
        public async Task AddPlayer(string role, string email, int id, string name, string teamName, string sport, int teamCode)
        {
            await firebase.Child("Users").PostAsync(new Users()
            {
                Role = role,
                Email = email,
                ID = id,
                Name = name,
                TeamName = teamName,
                Sport = sport,
                TeamCode = teamCode
            });
        }

        public async Task AddCoach(string role, string email, int id, string name, string teamName, string sport, int teamCode)
        {
            await firebase.Child("Users").PostAsync(new Users()
            {
                Role = role,
                Email = email,
                ID = id,
                Name = name,
                TeamName = teamName,
                Sport = sport,
                TeamCode = teamCode
            });
        }



        //This function is to add a team to the database
        public async Task AddTeam(string tn, int tc, string cn, string sp)
        {
            await firebase.Child("Teams").PostAsync(new Team()
            {
                TeamName = tn,
                TeamCode = tc,
                CoachName = cn,
                Sport = sp
            });
        }

        public async Task AddWorkout(string tm, string tc, string sp, string ty, string des)
        {
            await firebase.Child("Team Workouts").PostAsync(new TeamWorkouts()
            {
                UserComp = tm,
                TimeCreated = tc,
                Sport = sp,
                Type = ty,
                Description = des
            });
        }

        public async Task AddPersonalWorkout(string tm, string user, string tc, string sp, string ty, string des)
        {
            await firebase.Child("Personal Workouts").PostAsync(new Workouts()
            {
                UserComp = tm,
                WorkoutCompleter = user,
                TimeCreated = tc,
                Sport = sp,
                Type = ty,
                Description = des,
            });
        }


        //Gets the specific user from the database
        public async Task<Users> GetUser(string em)
        {
            var allUsers = await GetAllUsers();
            await firebase.Child("Users").OnceAsync<Users>();
            return allUsers.Where(c => c.Email == em).FirstOrDefault();
        }

        //Tests for a user with a specific ID
        public async Task<Users> GetPlayer(String Name)
        {
            var allUserTest = await GetAllUsers();
            await firebase.Child("Users").OnceAsync<Users>();
            return allUserTest.Where(c => c.Name == Name).FirstOrDefault();
        }


        //Searches for a team using the Team Code
        public async Task<Team> GetTeam(int tc)
        {
            var allTeams = await GetAllTeams();
            await firebase.Child("Teams").OnceAsync<Team>();
            return allTeams.Where(x => x.TeamCode == tc).FirstOrDefault();
        }

        //Updates the user information in Firebase
        public async Task UpdateUser(int id, string email, string name, string team, string role)
        {
            var toUpdateUser = (await firebase.Child("Users").OnceAsync<Users>()).Where(c => c.Object.ID == id).FirstOrDefault();

            await firebase.Child("Users").Child(toUpdateUser.Key).PutAsync(new Users()
            {
                Email = email,
                Name = name,
                TeamName = team,
                Role = role
            });
        }

        //Deletes a team from the system
        public async Task DeleteTeam(string tnam)
        {
            var delTeam = (await firebase.Child("Teams").OnceAsync<Team>()).Where(x => x.Object.TeamName == tnam).FirstOrDefault();

            await firebase.Child("Teams").Child(delTeam.Key).DeleteAsync();
        }

        //Deletes a User From the system
        public async Task DeleteUser(string em)
        {
            var delUser = (await firebase.Child("Users").OnceAsync<Users>()).Where(x => x.Object.Email == em).FirstOrDefault();

            await firebase.Child("Users").Child(delUser.Key).DeleteAsync();
        }

        //Deletes a workout that is being selected
        public async Task DeleteWorkout(string userComp, string timeCreated)
        {
            var delWorkout = (await firebase.Child("Team Workouts").OnceAsync<TeamWorkouts>()).Where(w => w.Object.UserComp == userComp).Where(x => x.Object.TimeCreated == timeCreated).FirstOrDefault();

            await firebase.Child("Team Workouts").Child(delWorkout.Key).DeleteAsync();
        }

        public async Task DeletePersonalWorkout(string userComp, string timeCreated)
        {
            var delpPWorkout = (await firebase.Child("Personal Workouts").OnceAsync<Workouts>()).Where(w => w.Object.UserComp == userComp).Where(x => x.Object.TimeCreated == timeCreated).FirstOrDefault();

            await firebase.Child("Personal Workouts").Child(delpPWorkout.Key).DeleteAsync();
        }
    }
}
