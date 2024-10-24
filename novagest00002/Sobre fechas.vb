Module Sobre_fechas
    ' Microsoft Visual Basic .NET cuenta con una clase para manejar valores de fecha y hora, es la clase: System.DateTime
    'Para crear una valor de fecha proporcionando por separado el año, el mes y el día usaremos
    'Dim fecha As Date = New Date(2014, 2, 21)
    'Nota: Date y DateTime son la misma función, en la anterior línea de código podríamos haber usado DateTime en vez de Date.
    'Para obtener la fecha actual del sistema usaremos
    'Dim fechaActual As Date = Date.Now
    'Para obtener la hora actual del sistema (incluyendo los ticks)
    'Dim hora As TimeSpan = Date.Now.TimeOfDay
    'Para obtener los días transcurridos desde el 1 de enero del año actual
    'Dim dias As Integer = Date.Today.DayOfYear
    'Para sumar un día a una fecha
    'Dim fechaManana As Date = Date.Today.AddDays(1)
    'Para restar un día a una fecha
    'Dim fechaAyer As Date = Date.Today.AddDays(-1)
    'Para obtener la hora que será cuando transcurran dos horas y media de la hora actual
    'Dim hora As TimeSpan = Date.Now.AddHours(2.5)
    'Para obtener la hora que será cuando transcurran dos días, 10 horas y 30 minutos desde la fecha actual
    'Dim hora As TimeSpan = Date.Now.Add(New TimeSpan(2, 10, 30, 0))
    'Para obtener la hora que fue hace un día, 12 horas y 30 minutos desde la fecha actual
    'Dim hora As TimeSpan = Date.Now.Subtract(New TimeSpan(1, 12, 30, 0))
    'Para obtener el número de días de un mes determinado
    'Dim dias As Integer = Date.DaysInMonth(2014, 2)
    'Para recuperar los nombres estándar o abreviados de los días de la semana y de los meses de acuerdo con la configuración local o el idioma
    'Dim mes As String
    '    For Each mes In DateTimeFormatInfo.CurrentInfo.AbbreviatedMonthNames
    '  Console.WriteLine(mes)
    'Next
    'Para obtener fechas en varios formatos
    'Dim fecha As Date = New Date(2014, 2, 21, 10, 12, 20, 500)
    'Console.WriteLine(fecha.ToShortDateString)
    'Console.WriteLine(fecha.ToLongDateString)
    'Console.WriteLine(fecha.ToShortTimeString)
    'Console.WriteLine(fecha.ToLongTimeString)
    'Console.WriteLine(fecha.ToFileTime)
    'Console.WriteLine(fecha.ToOADate)
    'Console.WriteLine(fecha.ToUniversalTime)
    'Console.WriteLine(fecha.ToLocalTime)
End Module
