Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System.Runtime.CompilerServices
Imports System.Numerics

' TODO:
' Look into these to implement standard forms A+Bi and A+iB for Complex, then R+Xj and R+jX for Impedance.
'   REF: Complex Struct
'   https://learn.microsoft.com/en-us/dotnet/api/system.numerics.complex?view=net-8.0
'   REF: System.Numerics.Complex struct
'   https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-numerics-complex
'   REF: Format a complex number
'   https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-numerics-complex#format-a-complex-number

Public Class ComplexStandardFormatter
    Implements IFormatProvider, ICustomFormatter

    '   REF: Format a complex number
    ' https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-numerics-complex#format-a-complex-number

    Private Const CHARI As System.Char = "i"c
    Private Const CHARMINUS As System.Char = "-"c
    Private Const CHARPLUS As System.Char = "+"c

    Public Function GetFormat(formatType As Type) As Object _
        Implements IFormatProvider.GetFormat

        If formatType Is GetType(ICustomFormatter) Then
            Return Me
        Else
            Return Nothing
        End If
    End Function

    Public Function Format(ByVal fmt As System.String,
                           ByVal arg As System.Object,
                           ByVal provider As IFormatProvider) _
        As System.String _
        Implements ICustomFormatter.Format

        If TypeOf arg Is System.Numerics.Complex Then
            Dim c1 As System.Numerics.Complex =
                DirectCast(arg, System.Numerics.Complex)

            ' Check if the format string has a precision specifier.
            Dim Precision As System.Int32
            Dim FmtString As System.String = System.String.Empty
            If fmt.Length > 3 Then
                Try
                    Precision = System.Int32.Parse(fmt.Substring(3))
                Catch e As System.FormatException
                    Precision = 0
                End Try
                FmtString = "N" + Precision.ToString()
            End If
            Dim Sign As System.Char =
                If(c1.Imaginary < 0.0, CHARMINUS, CHARPLUS)
            If fmt.Substring(0, 3).Equals("AIB",
                System.StringComparison.OrdinalIgnoreCase) Then

                Return c1.Real.ToString(FmtString) + Sign + CHARI +
                    Math.Abs(c1.Imaginary).ToString(FmtString)
            ElseIf fmt.Substring(0, 3).Equals("ABI",
                System.StringComparison.OrdinalIgnoreCase) Then

                Return c1.Real.ToString(FmtString) + Sign +
                    Math.Abs(c1.Imaginary).ToString(FmtString) + CHARI
            Else
                Return c1.ToString(fmt, provider)
            End If
        Else
            If TypeOf arg Is System.IFormattable Then
                Return DirectCast(
                    arg, System.IFormattable).ToString(fmt, provider)
            ElseIf arg IsNot Nothing Then
                Return arg.ToString()
            Else
                Return String.Empty
            End If
        End If
    End Function

End Class ' ComplexStandardFormatter

''' <summary>
''' This module contains extension methods for the
''' <see cref="System.Numerics.Complex"/> structure.
''' xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
''' </summary>
''' <remarks>
''' The extension methods are used to convert a <c>Complex</c> to its equivalent
''' string representation in the standard form of A + iB or A + Bi.
''' xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
''' </remarks>
Public Module Complex

    Private Const CHARI As System.Char = "i"c
    Private Const CHARMINUS As System.Char = "-"c
    Private Const CHARPLUS As System.Char = "+"c
    Private Const CHARUPPERE As System.Char = "E"c

#Region "ToString"

    ''' <summary>
    ''' Converts the value of a <see cref="System.Numerics.Complex"/> to its
    ''' equivalent string representation in the standard form of A+iB.
    ''' </summary>
    ''' <returns>The A+iB standard form representation of the current
    ''' <c>Complex</c>.</returns>
    <Extension()>
    Public Function ToStringAIB(ByVal aComplex As System.Numerics.Complex) _
        As System.String

        Dim Sign As System.String =
            If(aComplex.Imaginary < 0.0, CHARMINUS, CHARPLUS)
        Return $"{aComplex.Real}{Sign}{CHARI}{Math.Abs(aComplex.Imaginary)}"
    End Function ' ToStringAIB

    ''' <summary>
    ''' Converts the value of a <see cref="System.Numerics.Complex"/> to its
    ''' equivalent string representation in the standard form of A+Bi.
    ''' </summary>
    ''' <returns>The A+Bi standard form representation of the current
    ''' <c>Complex</c>.</returns>
    <Extension()>
    Public Function ToStringABI(ByVal aComplex As System.Numerics.Complex) _
        As System.String

        Dim Sign As System.String =
            If(aComplex.Imaginary < 0.0, CHARMINUS, CHARPLUS)
        Return $"{aComplex.Real}{Sign}{Math.Abs(aComplex.Imaginary)}{CHARI}"
    End Function ' ToStringABI

#End Region ' "ToString"

End Module ' Complex
