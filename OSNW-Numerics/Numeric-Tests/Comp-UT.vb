Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports Xunit
Imports OSNW.Numerics

Namespace Complex

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
            Dim Z As New System.Numerics.Complex(125.125, -50.5625)
            Assert.Equal(125.125, Z.Real)
            Assert.Equal(-50.5625, Z.Imaginary)
        End Sub

    End Class ' General

    Public Class ToString

        <Theory>
        <InlineData(125.125, 50.5625, "<125.125; 50.5625>")>
        <InlineData(-125.125, 50.5625, "<-125.125; 50.5625>")>
        <InlineData(125.125, -50.5625, "<125.125; -50.5625>")>
        <InlineData(-125.125, -50.5625, "<-125.125; -50.5625>")>
        <InlineData(Single.MaxValue, Single.MinValue, ' Encourage exponential notation.
                    "<3.4028234663852886E+38; -3.4028234663852886E+38>")>
        <InlineData(4444.125, 123_456.562_5, "<4444.125; 123456.5625>")> ' Underscore.
        <InlineData(5_555.125, 123_456.562_5, "<5555.125; 123456.5625>")> ' Underscore.
        Sub ToString_Generic_WorksAsExpected(
            ByVal real As Double, ByVal imaginary As Double, ByVal expected As String)

            Dim Z As New System.Numerics.Complex(real, imaginary)
            Dim Result As System.String = Z.ToString
            Assert.Equal(expected, Result)
        End Sub

        <Fact>
        Sub ToString_StandardForm_WorksAsExpected()

            Dim Cplx As New System.Numerics.Complex(12.1, -15.4)

            Dim S1 As String = $"Format AiBO0: " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:AiBO0}", Cplx)}"
            Dim S2 As String = $"Format AiBC3: " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:AiBC3}", Cplx)}"
            Dim S3 As String = $"Format ABiC0: " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:ABiC0}", Cplx)}"
            Dim S4 As String = $"Format ABiO3: " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:ABiO3}", Cplx)}"

            Dim Expect1 As String = "Format AiBO0: 12 - i15"
            Dim Expect2 As String = "Format AiBC3: 12.100-i15.400"
            Dim Expect3 As String = "Format ABiC0: 12-15i"
            Dim Expect4 As String = "Format ABiO3: 12.100 - 15.400i"

            Assert.Equal(Expect1, S1)
            Assert.Equal(Expect2, S2)
            Assert.Equal(Expect3, S3)
            Assert.Equal(Expect4, S4)

        End Sub

        '<Fact>
        'Sub ToStringAiBC_StandardForm_WorksAsExpected()

        '    Dim Cplx As New System.Numerics.Complex(12.1, -15.4)

        '    Dim OutStr As String = $"Format AiBC0: " +
        '        $"{String.Format(New ComplexStandardFormatter(), "{0:AiBC0}", Cplx)}"
        '    Dim S2 As String = $"Format AiBC3: " +
        '        $"{String.Format(New ComplexStandardFormatter(), "{0:AiBC3}", Cplx)}"

        '    Dim Expect1 As String = "Format AiBC0: 12-i15"
        '    Dim Expect2 As String = "Format AiBC3: 12.100-i15.400"

        '    Assert.Equal(Expect1, OutStr)
        '    Assert.Equal(Expect2, S2)

        'End Sub

        <Theory>
        <InlineData(125.125, -50.5625, 0, "125-i51")>
        <InlineData(-125.125, 50.5625, 3, "-125.125+i50.562")>
        <InlineData(4444.125, 123_456.562_5, 4, "4444.1250+i123456.5625")>
        <InlineData(5_555.125, 123_456.562_5, 4, "5555.1250+i123456.5625")> ' Underscore.
        Sub ToStringAiBC_StandardForm_WorksAsExpected(ByVal real As Double, ByVal imaginary As Double,
                                                      ByVal precision As UInt16, ByVal expected As String)

            Dim Cplx As New System.Numerics.Complex(real, imaginary)
            Dim FmtStr As String = $"{{0:AiBC{precision}}}"

            Dim OutStr As String = $"{String.Format(New ComplexStandardFormatter(), FmtStr, Cplx)}"

            Assert.Equal(expected, OutStr)

        End Sub

        '<Fact>
        'Sub ToStringAiBO_StandardForm_WorksAsExpected()

        '    Dim Cplx As New System.Numerics.Complex(12.1, -15.4)

        '    Dim S1 As String = $"Format AiBO0: " +
        '        $"{String.Format(New ComplexStandardFormatter(), "{0:AiBO0}", Cplx)}"
        '    Dim S2 As String = $"Format AiBO3: " +
        '        $"{String.Format(New ComplexStandardFormatter(), "{0:AiBO3}", Cplx)}"

        '    Dim Expect1 As String = "Format AiBO0: 12 - i15"
        '    Dim Expect2 As String = "Format AiBO3: 12.100 - i15.400"

        '    Assert.Equal(Expect1, S1)
        '    Assert.Equal(Expect2, S2)

        'End Sub

        <Theory>
        <InlineData(125.125, -50.5625, 0, "125 - i51")>
        <InlineData(-125.125, 50.5625, 3, "-125.125 + i50.562")>
        <InlineData(4444.125, 123_456.562_5, 4, "4444.1250 + i123456.5625")>
        <InlineData(5_555.125, 123_456.562_5, 4, "5555.1250 + i123456.5625")> ' Underscore.
        Sub ToStringAiBO_StandardForm_WorksAsExpected(ByVal real As Double, ByVal imaginary As Double,
                                                      ByVal precision As UInt16, ByVal expected As String)

            Dim Cplx As New System.Numerics.Complex(real, imaginary)
            Dim FmtStr As String = $"{{0:AiBO{precision}}}"

            Dim OutStr As String = $"{String.Format(New ComplexStandardFormatter(), FmtStr, Cplx)}"

            Assert.Equal(expected, OutStr)

        End Sub

        '<Fact>
        'Sub ToStringABiC_StandardForm_WorksAsExpected()

        '    Dim Cplx As New System.Numerics.Complex(12.1, -15.4)

        '    Dim S1 As String = $"Format ABiC0: " +
        '        $"{String.Format(New ComplexStandardFormatter(), "{0:ABiC0}", Cplx)}"
        '    Dim S2 As String = $"Format ABiC3: " +
        '        $"{String.Format(New ComplexStandardFormatter(), "{0:ABiC3}", Cplx)}"

        '    Dim Expect1 As String = "Format ABiC0: 12-15i"
        '    Dim Expect2 As String = "Format ABiC3: 12.100-15.400i"

        '    Assert.Equal(Expect1, S1)
        '    Assert.Equal(Expect2, S2)

        'End Sub

        <Theory>
        <InlineData(125.125, -50.5625, 0, "125-51i")>
        <InlineData(-125.125, 50.5625, 3, "-125.125+50.562i")>
        <InlineData(4444.125, 123456.5625, 4, "4444.1250+123456.5625i")>
        <InlineData(5_555.125, 123_456.562_5, 4, "5555.1250+123456.5625i")> ' Underscore.
        Sub ToStringABiC_StandardForm_WorksAsExpected(ByVal real As Double, ByVal imaginary As Double,
                                                      ByVal precision As UInt16, ByVal expected As String)

            Dim Cplx As New System.Numerics.Complex(real, imaginary)
            Dim FmtStr As String = $"{{0:ABiC{precision}}}"

            Dim OutStr As String = $"{String.Format(New ComplexStandardFormatter(), FmtStr, Cplx)}"

            Assert.Equal(expected, OutStr)

        End Sub

        '<Fact>
        'Sub ToStringABiO_StandardForm_WorksAsExpected()

        '    Dim Cplx As New System.Numerics.Complex(12.1, -15.4)

        '    Dim S1 As String = $"Format ABiO0: " +
        '        $"{String.Format(New ComplexStandardFormatter(), "{0:ABiO0}", Cplx)}"
        '    Dim S2 As String = $"Format ABiO3: " +
        '        $"{String.Format(New ComplexStandardFormatter(), "{0:ABiO3}", Cplx)}"

        '    Dim Expect1 As String = "Format ABiO0: 12 - 15i"
        '    Dim Expect2 As String = "Format ABiO3: 12.100 - 15.400i"

        '    Assert.Equal(Expect1, S1)
        '    Assert.Equal(Expect2, S2)

        'End Sub

        <Theory>
        <InlineData(125.125, -50.5625, 0, "125 - 51i")>
        <InlineData(-125.125, 50.5625, 3, "-125.125 + 50.562i")>
        <InlineData(4444.125, 123_456.562_5, 4, "4444.1250 + 123456.5625i")>
        <InlineData(5_555.125, 123_456.562_5, 4, "5555.1250 + 123456.5625i")> ' Underscore.
        Sub ToStringABiO_StandardForm_WorksAsExpected(ByVal real As Double, ByVal imaginary As Double,
                                                      ByVal precision As UInt16, ByVal expected As String)

            Dim Cplx As New System.Numerics.Complex(real, imaginary)
            Dim FmtStr As String = $"{{0:ABiO{precision}}}"

            Dim OutStr As String = $"{String.Format(New ComplexStandardFormatter(), FmtStr, Cplx)}"

            Assert.Equal(expected, OutStr)

        End Sub

        <Theory>
        <InlineData(125.125, 50.5625, OSNW.Numerics.StandardFormOrder.AiB,
                    OSNW.Numerics.StandardFormSpacing.Closed, 3, "125.125+i50.562")> ' Base
        <InlineData(-125.125, 50.5625, OSNW.Numerics.StandardFormOrder.AiB,
                    OSNW.Numerics.StandardFormSpacing.Closed, 3, "-125.125+i50.562")> ' -R
        <InlineData(125.125, -50.5625, OSNW.Numerics.StandardFormOrder.AiB,
                    OSNW.Numerics.StandardFormSpacing.Closed, 3, "125.125-i50.562")> ' -I
        <InlineData(125.125, 50.5625, OSNW.Numerics.StandardFormOrder.ABi,
                    OSNW.Numerics.StandardFormSpacing.Closed, 3, "125.125+50.562i")> ' ABi
        <InlineData(125.125, 50.5625, OSNW.Numerics.StandardFormOrder.AiB,
                    OSNW.Numerics.StandardFormSpacing.Open, 3, "125.125 + i50.562")> ' Open
        <InlineData(125.125, 50.5625, OSNW.Numerics.StandardFormOrder.AiB,
                    OSNW.Numerics.StandardFormSpacing.Closed, 0, "125+i51")> ' Precision 0
        <InlineData(4444.125, 123_456.562_5, OSNW.Numerics.StandardFormOrder.AiB,
                    OSNW.Numerics.StandardFormSpacing.Closed, 4, "4444.1250+i123456.5625")> ' Underscore.
        <InlineData(5_555.125, 123_456.562_5, OSNW.Numerics.StandardFormOrder.AiB,
                    OSNW.Numerics.StandardFormSpacing.Closed, 4, "5555.1250+i123456.5625")> ' Underscore.
        Sub ToStringStandard_Detailed_WorksAsExpected(
            ByVal real As Double, ByVal imaginary As Double,
            ByVal order As StandardFormOrder, ByVal spacing As StandardFormSpacing,
            ByVal precision As System.UInt16, ByVal expected As String)

            Dim Cplx As New System.Numerics.Complex(real, imaginary)
            'Dim FmtStr As String = $"{{0:ABiO{precision}}}"

            Dim OutStr As String = Cplx.ToStandardString(order, spacing, precision)

            Assert.Equal(expected, OutStr)

        End Sub

    End Class ' ToString

End Namespace ' Complex
