<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccProjectSchdule.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptAccProjectSchdule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
  <style>
.switch {
  position: relative;
  display: inline-block;
  width: 60px;
  height: 30px;
}

.switch input { 
  opacity: 0;
  width: 0;
  height: 0;
}

.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #ccc;
  -webkit-transition: .4s;
  transition: .4s;
}

.slider:before {
  position: absolute;
  content: "";
  height: 26px;
  width: 26px;
  left: 4px;
  bottom: 4px;
  background-color: white;
  -webkit-transition: .4s;
  transition: .4s;
}

input:checked + .slider {
  background-color: #2196F3;
}

input:focus + .slider {
  box-shadow: 0 0 1px #2196F3;
}

input:checked + .slider:before {
  -webkit-transform: translateX(26px);
  -ms-transform: translateX(26px);
  transform: translateX(26px);
}

/* Rounded sliders */
.slider.round {
  border-radius: 34px;
}

.slider.round:before {
  border-radius: 50%;
}
        </style>

    <script type="text/javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });

            var gvproschdule = $('#<%=this.gvproschdule.ClientID%>');



            gvproschdule.gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 3


            });

        }

    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <asp:Label ID="lbljavascript" runat="server"></asp:Label>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblSalesTeam" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcProject" runat="server" TabIndex="3" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                        <asp:LinkButton ID="imgbtnFindProject" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                    </div>


                                    <cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black" CssClass=" "
                                        MaxDropDownHeight="200" TabIndex="8" TransitionalMode="True" Width="360px">
                                    </cc1:DropCheck>




                                    <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn pull-right"
                                        Style="margin: -20px 0 0 370px; float: left !important;" TabIndex="9"></asp:LinkButton>





                                </div>
                                <div class="form-group">
                                    <div class="col-md-4 pading5px  asitCol4">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtfrmdate" runat="server" TabIndex="3" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>

                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" TabIndex="3" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                    </div>
                                    <div class="col-md-4 pading5px  asitCol4">
                                      

                                          <label id="lblwithopening" runat="server"
                                            class="switch">
                                            <asp:CheckBox ID="chkboxwithOpening" runat="server" />

                                            <span class="btn btn-xs slider round"></span>
                                        </label>
                                        <asp:Label runat="server" Text="With Opening" CssClass="btn btn-xs"></asp:Label>

                                          <asp:Label ID="lbltkinlac" runat="server" CssClass="lblTxt lblName" Text="Taka in LAC"></asp:Label>
                                    </div>

                                   

                                    <div class="clearfix"></div>


                                </div>
                        </fieldset>
                    </div>


                    <div class="row">

                        <asp:GridView ID="gvproschdule" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvproschdule_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField FooterText="Total" HeaderText=" Description">
                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Height="16px" Text="Description"
                                                        Width="180px" Font-Size="14px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnCdataExel" runat="server" CssClass="btn btn-warning primaryBtn">Export Exel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActDescCost" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle Font-Bold="True" Font-Size="14px" ForeColor="Black" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoCost" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtopamtcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "topamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P1">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC1" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc1" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p1")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P2">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc2" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p2")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC2" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P3">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc3" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC3" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P4">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc4" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC4" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P5">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc5" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC5" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P6">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc6" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p6")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC6" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P7">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc7" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p7")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC7" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P8">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc8" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p8")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC8" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P9">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc9" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p9")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC9" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P10">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc10" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p10")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC10" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P11">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc11" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p11")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC11" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P12">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc12" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p12")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC12" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P13">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc13" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p13")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC13" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P14">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc14" runat="server" Style="text-align: right"  Font-Size="12px"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p14")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC14" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P15">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc15" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p15")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC15" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P16">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc16" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p16")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC16" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P17">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc17" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p17")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC17" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P18">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc18" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p18")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC18" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P19">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc19" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p19")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC19" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P20">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc20" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p20")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC20" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P21">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc21" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p21")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC21" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P22">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc22" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p22")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC22" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P23">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc23" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p23")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC23" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P24">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc24" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p24")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC24" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P25">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc25" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p25")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC25" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P26">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc26" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p26")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC26" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P27">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc27" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p27")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC27" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P28">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc28" runat="server" Style="text-align: right"  Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p28")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC28" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P29">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc29" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p29")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC29" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P30">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc30" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p30")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC30" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P31">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc31" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p31")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC31" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P32">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc32" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p32")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC32" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P33">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc33" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p33")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC33" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P34">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc34" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p34")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC34" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P35">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc35" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p35")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC35" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P36">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc36" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p36")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC36" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P37">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc37" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p37")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC37" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P38">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc38" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p38")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC38" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P39">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc39" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p39")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC39" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P40">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc40" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p40")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC40" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P41">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc41" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p41")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC41" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P42">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc42" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p42")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC42" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P43">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc43" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p43")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC43" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P44">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc44" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p44")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC44" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P45">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc45" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p45")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC45" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P46">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc46" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p46")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC46" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P47">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc47" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p47")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC47" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P48">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc48" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p48")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC48" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P49">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc49" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p49")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC49" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P50">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc50" runat="server" Style="text-align: right" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p50")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC50" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P51">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC51" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc51" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p51")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P52">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc52" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p52")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC52" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P53">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc53" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p53")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC53" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P54">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc54" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p54")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC54" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P55">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc55" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p55")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC55" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P56">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc56" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p56")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC56" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P57">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc57" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p57")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC57" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P58">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc58" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p58")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC58" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P59">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc59" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p59")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC59" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P60">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc60" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p60")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC60" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P61">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc61" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p61")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC61" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P62">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc62" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p62")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC62" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P63">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc63" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p63")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC63" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P64">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc64" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p64")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC64" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P65">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc65" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p65")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC65" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P66">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc66" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p66")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC66" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P67">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc67" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p67")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC67" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P68">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc68" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p68")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC68" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P69">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc69" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p69")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC69" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P70">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc70" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p70")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC70" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P71">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc71" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p71")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC71" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P72">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvp72" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p72")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC72" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P73">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc73" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p73")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC73" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P74">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc74" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p74")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC74" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P75">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc75" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p75")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC75" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P76">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc76" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p76")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC76" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P77">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc77" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p77")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC77" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P78">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc78" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p78")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC78" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P79">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc79" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p79")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC79" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P80">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc80" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p80")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC80" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P81">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc81" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p81")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC81" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P82">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc82" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p82")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC82" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P83">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc83" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p83")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC83" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P84">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc84" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p84")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC84" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P85">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc85" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p85")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC85" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P86">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc86" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p86")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC86" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P87">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc87" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p87")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC87" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P88">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc88" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p88")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC88" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P89">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc89" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p89")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC89" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P90">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc90" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p90")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC90" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P91">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc91" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p91")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC91" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P92">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc92" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p92")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC92" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P93">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc93" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p93")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC93" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P94">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc94" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p94")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC94" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P95">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc95" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p95")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC95" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P96">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc96" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p96")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC96" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P97">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc97" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p97")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC97" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P98">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc98" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p98")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC98" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P99">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc99" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p99")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC99" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P100">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpc100" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p100")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPC100" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="55px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>


                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

