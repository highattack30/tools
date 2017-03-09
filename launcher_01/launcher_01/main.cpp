#include <Windows.h>
#include <string>
#include <fstream>

int main(int argc, char * argv[])
{
	STARTUPINFO a;
	GetStartupInfo(&a);



	LPSTR aa = GetCommandLine();

	std::ofstream out = std::ofstream("out_data.txt");
	out << a.cb;
	out << '\n';

	out << a.cbReserved2;
	out << '\n';

	out << a.dwFillAttribute;
	out << '\n';

	out << a.dwX;
	out << '\n';

	out << a.lpTitle;
	out << '\n';

	out << a.wShowWindow;
	out << '\n';
	out.close();

	MessageBox(0, aa, 0, MB_OK);
	return 0;
}