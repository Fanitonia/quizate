using System;
using Microsoft.EntityFrameworkCore;

namespace Quizate.API.Data;

public class QuizateDbContext(DbContextOptions<QuizateDbContext> options): DbContext(options)
{

}
