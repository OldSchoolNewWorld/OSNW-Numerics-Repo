Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports Xunit

Namespace Impedance

    ' NOTE: Test values should be comprised of 1/2, 1/4, 1/8, ... to avoid
    ' failures due binary storage of fractional parts.

    ' Use this to run ad hoc tests.
    Public Class AdHoc
        '
        '
        '
        '
        '
    End Class

    Public Class General

        <Fact>
        Sub Constructor_WorksAsExpected()
            Dim Z As New OSNW.Numerics.Impedance(125.125, -50.5)
            Assert.Equal(125.125, Z.Resistance)
            Assert.Equal(-50.5, Z.Reactance)
        End Sub

    End Class ' General

    Public Class ToString

        <Theory>
        <InlineData(125.125, 50.5, "<125.125; 50.5>")>
        <InlineData(-125.125, 50.5, "<-125.125; 50.5>")>
        <InlineData(125.125, -50.5, "<125.125; -50.5>")>
        <InlineData(-125.125, -50.5, "<-125.125; -50.5>")>
        <InlineData(Single.MaxValue, Single.MinValue, ' Encourage exponential notation.
                    "<3.4028234663852886E+38; -3.4028234663852886E+38>")>
        Sub ToString_Generic_WorksAsExpected(
            ByVal resistance As Double, ByVal reactance As Double, ByVal expected As String)

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
