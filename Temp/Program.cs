namespace Temp
{
    internal class Program
    {

         static void Main(string[] args)
        {
            //"Adatbázis" ///////////////////////////////
												List<Teacher> teachers = new List<Teacher>();
												List<Student> students = new List<Student>();
												List<Admin> admins = new List<Admin>();
												List<Course> courses = new List<Course>();
            /////////////////////////////////////////////
            
            Setup(teachers,students,courses,admins); //Feltölti alap adatokkal az "adatbázist".
            Boolean run = true;

            while (run) //Fő menü loop
            {
               
                Console.Clear();
                Console.WriteLine("Válasszon a menüből:\n1. Belépés diákként\n2. Belépés adminisztrátorként\n3. Kilépés");
                switch (Console.ReadLine())
                {
                case "1": LoginStudent(students,courses); break;
                case "2": LoginAdmin(admins,students,courses); break;
                case "3": run = false; break;
                default: break;

                }
            }
        }

        /// Adminisztratív függvények
        public static void CourseQuit(List<Course>courses,string courseId,string userId)
        {
												foreach (Course course in courses)
												{

																if (course.Id == courseId)
																{
																				course.RegStudents.Remove(userId);
                    Console.WriteLine($"{userId} kódú tanuló sikeresen leadta a(z) {course.Name} tárgyat! ");
                    Console.ReadKey();

																}

												}
								}
        public static void CourseEnter(List<Course>courses,List<Student>students,String courseId,String userId)
        {
            Boolean run = false;

            foreach (Student student in students)
            {
                if (student.Id == userId) run = true;
            }
            if (run)
            {
                foreach (Course course in courses)
                {

                    if (course.Id == courseId)
                    {
                        if (course.RegStudents.Contains(userId))
                        {
                            Console.WriteLine($"A hallgató már felvette ezt a(z) {course.Name} tárgyat!");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            course.RegStudents.Add(userId);
                            Console.WriteLine($"{userId} kódú hallgató sikeresen felvette a(z) {course.Name} tárgyat!");
                            Console.ReadKey();
                            
                        }

                    }

                }
            }
            if (!run)
            {
                Console.WriteLine($"HIBA! A keresett hallgató ({userId}) nem található a rendszerben!");
                Console.ReadKey();
            }
        }
        public static void ListStudents(List<Student> students)
            {
            Console.Clear();
            foreach (Student student in students) Console.WriteLine($"Név: {student.Name} | Azonosító: {student.Id} | Státusz: {student.Status}");
            Console.WriteLine("\nA továbblépéshez nyomjon ENTERT");
            Console.ReadKey();
            }
        public static void ListCourses(List<Course>courses)
        {
												Console.Clear();
												foreach (Course course in courses) Console.WriteLine($"Név: {course.Name} | Azonosító: {course.Id} | Kredit: {course.Credit}");
												Console.WriteLine("\nA továbblépéshez nyomjon ENTERT");
												Console.ReadKey();
								}
        public static void ChangeStatus(List<Student> students, String usredId)
        {
            foreach (Student student in students)
            {
                if(student.Id == usredId) 
                { 
                student.Status = !student.Status;
                    Console.WriteLine($"\n{student.Name} ({student.Id}) hallgató státusza megváltozott!\nÚj státusza: {student.Status}");
                    Console.ReadKey();
                }
            }
        }
       
        ///Felhasználói felületek
        public static void LoginStudent(List<Student>students,List<Course>courses)
        {
            Boolean run = false;
            Student user = students[0];

            while (user.Name == "NULL") //Az első loop. Itt választjuk ki a használni kívánt fiókot diákként
            {
                Console.Clear();
                Console.Write("Adja meg az azonosító kódját(LM45T6) vagy kilépéshez \"EXIT\": ");
                String input = Console.ReadLine();
                if (input == "EXIT") break;

                foreach (Student student in students)
                {
                    if (student.Id == input)
                    {
                        user = student;
                        run = true;
                    }
                }
                if (run) break;
                Console.WriteLine("Helytelen kódot adott meg! Próbálja újra!");
            }

            while(run)
            {
                Console.Clear() ;
																Console.WriteLine($"Üdvözlöm {user.Name} ({user.Id}) | Státusz: {user.Status} |\n");
                Console.WriteLine("Válasszon a menűből:\n1. Kilépés\n2. Státusz átváltása\n3. Kurzusra jelentkezés\n4. Kurzus leadása\n5. Kurzusok listája\n");

                switch(Console.ReadLine())
                {
																				case ("1"): run = false; break;
																				case ("2"): ChangeStatus(students,user.Id); break;
																				case ("3"): 
                        {
                            Console.WriteLine("Adja meg a felvenni kívánt kurzus 4 jegyű kódját: ");
                            CourseEnter(courses,students, Console.ReadLine(), user.Id);
                        } break;
                    case ("4"):
																								{
																												Console.WriteLine("Adja meg a leadni kívánt kurzus 4 jegyű kódját: ");
																												CourseQuit(courses, Console.ReadLine(), user.Id);
																								}break;
																				case ("5"):
																								ListCourses(courses); break;
																								
																				
                    default: break;
																}

												}

        }
								public static void LoginAdmin(List<Admin>admins,List<Student>students,List<Course>courses)
								{
												Boolean run = false;
												Admin user = admins[0];
												while (user.Name == "NULL") //Az első loop. Itt választjuk ki a használni kívánt fiókot diákként
												{   Console.Clear();
																Console.Write("Adja meg az azonosító kódját(G0NBC4) vagy kilépéshez \"EXIT\": ");
																String input = Console.ReadLine();
																if (input == "EXIT") break;

																foreach (Admin admin in admins)
																{
																				if (admin.Id == input)
																				{
																								user = admin;
																								run = true;
																				}
																}
																if (run) break;
																Console.WriteLine("Helytelen kódot adott meg! Próbálja újra!");
												}
												while (run){ 
																Console.Clear();
																Console.WriteLine($"Üdvözlöm {user.Name} ({user.Id})\n");
                Console.WriteLine("Válasszon a menűből:\n1. Kilépés\n2. Hallgató státuszának váltása\n3. Hallgatók listája\n4. Hallgató kurzusra vétele");
                Console.WriteLine("5. Hallgató eltávolítása egy kurzusról\n6. Kurzusok listája");
																switch (Console.ReadLine())
																{
																				case ("1"): run = false; break;
																				case ("2"): 
                        { 
                            Console.WriteLine("Kérem adja meg a hallgató 6 jegyű azonosítóját: ");
                            ChangeStatus(students, Console.ReadLine());
                        } break;
																				case("3"): ListStudents(students); break;
                    case("4"):
                        {
																												Console.WriteLine("Kérem adja meg a tanuló 6 jegyű azonosítóját: ");
																												String tmp = Console.ReadLine();
																												Console.WriteLine("Kérem adja meg a kurzus 4 jegyű azonosítóját: ");
                            CourseEnter(courses,students,Console.ReadLine(),tmp);
																								} break;
                    case("5"):
																								{
																												Console.WriteLine("Kérem adja meg a tanuló 6 jegyű azonosítóját: ");
																												String tmp = Console.ReadLine();
																												Console.WriteLine("Kérem adja meg a kurzus 4 jegyű azonosítóját: ");
																												CourseQuit(courses,Console.ReadLine(),tmp);
																								}
																								break;
																				case ("6"): ListCourses(courses); break;

																				default: break;
																}


												}
								}
								
        ///Segéd függvények
        public static void Setup(List<Teacher> teachers, List<Student> students, List<Course> courses, List<Admin> admins)
								{
												///////// Ez egy segéd függvény, amely feltölti az adatbázist kezdő adatokkal /////////

												teachers.Add(new Teacher("Kovács Gábor", "AB12C3"));
												teachers.Add(new Teacher("Papp Laura", "XY98Z1"));

												students.Add(new Student(false, "NULL", "NULL",0));
												students.Add(new Student(true, "Horváth Réka", "LM45T6", 0));
												students.Add(new Student(true, "Takács Levente", "PQ77B2", 0));

												courses.Add(new Course(teachers[0], "Mesterséges Intelligencial Alapjai", "MIAI", 3, new List<String>()));
												courses.Add(new Course(teachers[1], "Operációs Rendszerek", "OPRE", 4, new List<String>()));

												admins.Add(new Admin("NULL", "NULL"));
												admins.Add(new Admin("Németh Márk", "JK31D4"));
												admins.Add(new Admin("Tóth Ivett", "G0NBC4"));

								}
				}


    abstract class Person
    {
        public String Name { get; set; }
        public string Id {  get; set; }
       
    }
    class Student : Person
    {
        public Boolean Status {  get; set; }
								public int Credits { get; set; }
								public Student(Boolean status,String name,string id,int credits)
        {
            this.Status = status;
            this.Name = name;
            this.Id = id;
            this.Credits = credits;
        }

    }
    class Teacher : Person
    {
        
        public Teacher(String name, string id)
        {
            
            this.Name = name;
            this.Id = id;
        }
				}
				class Admin : Person
				{
        public Admin(String name, string id)
        {
        this.Name = name;
        this.Id = id;
        }

				}
    class Course
    { 
        public Teacher Teacher { get; set; }
        public String Name { get; set; }
        public String Id { get; set; }
        public int Credit { get; set; }
        public List<String> RegStudents { get; set; }
								public Course(Teacher teacher, String name, String id, int credit, List<String> regStudents)
        {
            this.Teacher = teacher;
            this.Name = name;
            this.Id = id;
            this.Credit = credit;
            this.RegStudents = regStudents;

								}
    }

}
