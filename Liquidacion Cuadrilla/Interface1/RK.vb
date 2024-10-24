Public Class RK
    Dim P(54) As Byte
    Dim Pila0(2, 0) As String
    Dim Pila1(2, 0) As String

    'Para el elemento i en la pila, Pilax(0 , i) es la transformación, p.ej. "RUflL..."

    '                               Pilax(1 , i) es la posición después de la transformación expresada como un array de 54 valores, p.ej. "1252032..." 
    '                               Cada valor indica un color (0=Blanco, 1 = Rojo, 2 = Amarillo, 3 = Naranja, 4 = Azul , 5 = Verde

    '                               Pilax(2,  i) es la posición de cada pieza después de la transformación, expresada como un array de 26 valores
    '                                                                  0   1  2   3 4  5  6   7  8    9 10  11  12 13  14   15 16 17  18 19  20  21 22 23  24  25
    '                                                                 ULB UB URB UL U UR UFL UF URF  FL  F  FR FDL FD FRD   DL  D DR DBL DB DBR  BL  B BR   R   L



    Private Sub RK_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class