using UserRegistration.Models;
using System.Data;
using System.Data.SqlClient;

namespace UserRegistration.DAL
{
    public class User_DAL
    {
        private SqlConnection ? _connection;
        private SqlCommand ? _command;
        public static IConfiguration ? Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public List<User> GetAll()
        {
            List<User> userList = new List<User>();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "SPS_Users";
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();
                while (dr.Read())
                {
                    User user = new User
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),
                        FullName = dr["FullName"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]).Date,
                        State = dr["State"].ToString(),
                        District = dr["District"].ToString(),
                        Email = dr["Email"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString()
                    };
                    userList.Add(user);
                }
                _connection.Close();
            }
            return userList;
        }

        public bool Insert(User model)
        {
            int id;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "SPI_Users";
                _command.Parameters.AddWithValue("@FullName", model.FullName ?? (object)DBNull.Value);
                _command.Parameters.AddWithValue("@Gender", model.Gender ?? (object)DBNull.Value);
                _command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                _command.Parameters.AddWithValue("@State", model.State ?? (object)DBNull.Value);
                _command.Parameters.AddWithValue("@District", model.District ?? (object)DBNull.Value);
                _command.Parameters.AddWithValue("@Email", model.Email ?? (object)DBNull.Value);
                _command.Parameters.AddWithValue("@Phone", model.Phone ?? (object)DBNull.Value);
                _command.Parameters.AddWithValue("@Username", model.Username ?? (object)DBNull.Value);
                _command.Parameters.AddWithValue("@Password", model.Password ?? (object)DBNull.Value);
                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0;
        }

        public User GetById(int id)
        {
            User user = new User();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "SPS_UsersById";
                _command.Parameters.AddWithValue("@UserId", id);
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();
                while (dr.Read())
                {
                    user.UserId = Convert.ToInt32(dr["UserId"]);
                    user.FullName = dr["FullName"].ToString();
                    user.Gender = dr["Gender"].ToString();
                    user.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]).Date;
                    user.State = dr["State"].ToString();
                    user.District = dr["District"].ToString();
                    user.Email = dr["Email"].ToString();
                    user.Phone = dr["Phone"].ToString();
                    user.Username = dr["Username"].ToString();
                    user.Password = dr["Password"].ToString();
                }
                _connection.Close();
            }
            return user;
        }

        public bool Update(User model)
        {
            int id;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "SPU_Users";
                _command.Parameters.AddWithValue("@UserId", model.UserId);
                _command.Parameters.AddWithValue("@FullName", model.FullName ?? (object)DBNull.Value);
                _command.Parameters.AddWithValue("@Gender", model.Gender ?? (object)DBNull.Value);
                _command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                _command.Parameters.AddWithValue("@State", model.State ?? (object)DBNull.Value);
                _command.Parameters.AddWithValue("@District", model.District ?? (object)DBNull.Value);
                _command.Parameters.AddWithValue("@Email", model.Email ?? (object)DBNull.Value);
                _command.Parameters.AddWithValue("@Phone", model.Phone ?? (object)DBNull.Value);
                _command.Parameters.AddWithValue("@Username", model.Username ?? (object)DBNull.Value);
                _command.Parameters.AddWithValue("@Password", model.Password ?? (object)DBNull.Value);
                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0;
        }

        public bool Delete(int id)
        {
            int deletedRowCount;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "SPD_Users";
                _command.Parameters.AddWithValue("@UserId", id);
                _connection.Open();
                deletedRowCount = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return deletedRowCount > 0;
        }
    }
}
