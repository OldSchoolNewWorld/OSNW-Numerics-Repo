Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports Xunit

Namespace Impedance

    Public Class ToString

        <Theory>
        <InlineData(125.125, 50.5, "<125.125; 50.5>")>
        <InlineData(-125.125, 50.5, "<-125.125; 50.5>")>
        <InlineData(125.125, -50.5, "<125.125; -50.5>")>
        <InlineData(-125.125, -50.5, "<-125.125; -50.5>")>
        <InlineData(Single.MaxValue, Single.MinValue,
                    "<3.4028234663852886E+38; -3.4028234663852886E+38>")> ' Encourage exponential notation
        Sub Works_AsExpected(ByVal resistance As Double, ByVal reactance As Double, ByVal expected As String)
            Dim Z As New OSNW.Numerics.Impedance(resistance, reactance)
            Dim Result As System.String = Z.ToString
            Assert.Equal(expected, Result)
        End Sub

    End Class ' ToString

    Public Class ToStringRjX

        <Theory>
        <InlineData(125.125, 50.5, "125.125+j50.5")>
        <InlineData(-125.125, 50.5, "-125.125+j50.5")>
        <InlineData(125.125, -50.5, "125.125-j50.5")>
        <InlineData(-125.125, -50.5, "-125.125-j50.5")>
        <InlineData(Single.MaxValue, Single.MinValue,
                    "3.4028234663852886E+38-j3.4028234663852886E+38")> ' Encourage exponential notation
        Sub Works_AsExpected(ByVal resistance As Double, ByVal reactance As Double, ByVal expected As String)
            Dim Z As New OSNW.Numerics.Impedance(resistance, reactance)
            Dim Result As System.String = Z.ToStringRjX
            Assert.Equal(expected, Result)
        End Sub

    End Class ' ToStringRjX

    Public Class ToStringRXj

        <Theory>
        <InlineData(125.125, 50.5, "125.125+50.5j")>
        <InlineData(-125.125, 50.5, "-125.125+50.5j")>
        <InlineData(125.125, -50.5, "125.125-50.5j")>
        <InlineData(-125.125, -50.5, "-125.125-50.5j")>
        <InlineData(Single.MaxValue, Single.MinValue,
                    "3.4028234663852886E+38-3.4028234663852886E+38j")> ' Encourage exponential notation
        Sub Works_AsExpected(ByVal resistance As Double, ByVal reactance As Double, ByVal expected As String)
            Dim Z As New OSNW.Numerics.Impedance(resistance, reactance)
            Dim Result As System.String = Z.ToStringRXj()
            Assert.Equal(expected, Result)
        End Sub

    End Class ' ToStringRXj

End Namespace ' Impedance
