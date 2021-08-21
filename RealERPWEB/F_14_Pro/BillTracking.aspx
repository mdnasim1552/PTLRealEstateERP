<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="BillTracking.aspx.cs" Inherits="RealERPWEB.F_14_Pro.BillTracking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

    
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            var gv = $('#<%=this.gvBillTracking.ClientID %>');
            gv.Scrollable();
        }
    </script>

    <style>
        .fieldset_A .form-horizontal .checkbox,.fieldset_A .form-horizontal .checkbox-inline, .fieldset_A .form-horizontal .radio, .fieldset_A .form-horizontal .radio-inline{
            padding:0;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                          <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
                            <asp:Panel ID="Panel8" runat="server">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3 ">
                                        <asp:Label ID="Label12" runat="server" CssClass="btn btn-success primaryBtn" Text="Field Info:"></asp:Label>

                                        <asp:CheckBox ID="chkallBillTracking" runat="server" style="margin-left:30px;" AutoPostBack="True" CssClass="checkbox-inline margin5px"
                                           
                                            OnCheckedChanged="chkallBillTracking_CheckedChanged" Text="Check All"
                                            Width="50px" />
                                        </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblfdate" runat="server" CssClass=" smLbl_to" Text="From:"></asp:Label>

                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat=""></cc1:CalendarExtender>

                                        <asp:Label ID="tDate" runat="server" CssClass=" smLbl_to"> To</asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy " TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>

                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-12 pading5px">
                                         <asp:CheckBoxList ID="cblBillTracking" runat="server" CssClass="rbtnList1 chkBoxControl margin5px"
                                        OnSelectedIndexChanged="cblBillTracking_SelectedIndexChanged"
                                        AutoPostBack="True" RepeatDirection="Horizontal" RepeatColumns="8">
                                        <asp:ListItem>aa</asp:ListItem>
                                        <asp:ListItem>bb</asp:ListItem>
                                        <asp:ListItem>cc</asp:ListItem>
                                        <asp:ListItem>dd</asp:ListItem>
                                        <asp:ListItem>ee</asp:ListItem>
                                        <asp:ListItem>ff</asp:ListItem>
                                        <asp:ListItem>gg</asp:ListItem>
                                        <asp:ListItem>hh</asp:ListItem>
                                        <asp:ListItem>mm</asp:ListItem>
                                    </asp:CheckBoxList>

                                    </div>

                                </div>
                            </asp:Panel>

                            <div class="form-group">
                                    <fieldset class="scheduler-border fieldset_A">

                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-4 pading5px asitCol4">

                                                    <asp:Label ID="lblSearchlist" runat="server" CssClass="lblTxt lblName"
                                                        Text="Search List"></asp:Label>

                                                    <asp:DropDownList ID="ddlFieldList1" runat="server" Width="152px" CssClass=" ddlPage inputTxt" >
                                                    </asp:DropDownList>

                                                    <div class="clearfix"></div>
                                                    <div class="form-group">

                                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName"
                                                            Text=""></asp:Label>

                                                        <asp:DropDownList ID="ddlFieldList2" runat="server" Width="152px"  CssClass=" ddlPage inputTxt">
                                                        </asp:DropDownList>


                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName"
                                                            Text=""></asp:Label>
                                                        <asp:DropDownList ID="ddlFieldList3" runat="server" Width="152px" CssClass=" ddlPage inputTxt">
                                                        </asp:DropDownList>


                                                    </div>



                                                </div>

                                                <div class="col-md-1 pading5px">
                                                    <div>
                                                        <asp:DropDownList ID="ddlSrch1" runat="server" Width="90px" CssClass=" ddlistPull">
                                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                            <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div class="form-group">

                                                        <asp:DropDownList ID="ddlSrch2" runat="server"  Width="90px" CssClass=" ddlistPull">
                                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                            <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:DropDownList ID="ddlSrch3" runat="server" Width="90px"  CssClass=" ddlistPull">
                                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                            <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>


                                                <div class="col-md-2 pading5px asitCol2">
                                                   <%-- <asp:Label ID="lbland1" runat="server" CssClass="lblTxt lblName" Text="And" Visible="False"
                                                        Width="25px"></asp:Label>


                                                    <asp:TextBox ID="txttoSearch1" runat="server" CssClass="txtboxformat"></asp:TextBox>

                                                    <asp:DropDownList ID="ddltodesig1" runat="server" CssClass=" ddlPage62 inputTxt">
                                                    </asp:DropDownList>--%>

                                                    <div class="form-group">
                                                         <asp:TextBox ID="txtSearch2" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlOperator1" runat="server" CssClass="ddlPage62 inputTxt pull-right">
                                                        <asp:ListItem Value="and">And</asp:ListItem>
                                                        <asp:ListItem Value="or">Or</asp:ListItem>
                                                    </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group">
                                                    <asp:TextBox ID="txtSearch1" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                    <asp:DropDownList ID="ddldesig01" runat="server" CssClass=" ddlPage62 inputTxt pull-right">
                                                        <asp:ListItem Value="and">And</asp:ListItem>
                                                        <asp:ListItem Value="or">Or</asp:ListItem>
                                                    </asp:DropDownList>
                                                        </div>
                                                    <div class="clearfix"></div>

                                                    <div class="form-group">
                                                   
                                                   <%-- <asp:DropDownList ID="ddldesig02" runat="server"
                                                        CssClass="ddlPage62 inputTxt">
                                                    </asp:DropDownList>--%>
                                                        </div>

                                                 <%--   <asp:Label ID="lbland2" runat="server" Text="And" Visible="False"
                                                        CssClass="lblTxt lblName"></asp:Label>

                                                    <asp:TextBox ID="txttoSearch2" runat="server" CssClass="txtboxformat"></asp:TextBox>

                                                    <asp:DropDownList ID="ddltodesig2" runat="server" CssClass=" ddlPage62 inputTxt">
                                                    </asp:DropDownList>--%>

                                                    <div class="form-group">    
                                                          <asp:TextBox ID="txtSearch3" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                        <asp:DropDownList ID="ddlOperator2" runat="server" CssClass="ddlPage62 inputTxt pull-right" Visible="false">
                                                        <asp:ListItem Value="and">And</asp:ListItem>
                                                        <asp:ListItem Value="or">Or</asp:ListItem>
                                                    </asp:DropDownList>
                                                    </div>



<%--                                                    <asp:Label ID="lbland3" runat="server" Text="And" Visible="False"
                                                        CssClass="lblTxt lblName"></asp:Label>

                                                    <asp:TextBox ID="txttoSearch3" runat="server" CssClass="txtboxformat"></asp:TextBox>

                                                    <asp:DropDownList ID="ddltodesig3" runat="server" CssClass=" ddlPage62 inputTxt">
                                                    </asp:DropDownList>--%>




                                                    <div class="clearfix"></div>

                                                  <%-- 
                                                    <asp:DropDownList ID="ddldesig03" runat="server" CssClass="ddlPage62 inputTxt">
                                                    </asp:DropDownList>--%>
                                                </div>


                                                <div class="col-md-5 pading5px">
                                                    <asp:Panel ID="Panel5" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblOrderList" runat="server" CssClass=" smLbl_to"
                                                                        Text="Order Field:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlOrder1" runat="server" CssClass="ddlistPull" Width="150px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlOrderad1" runat="server"
                                                                        CssClass="ddlPage62">
                                                                        <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                                        <asp:ListItem Value="desc">Des</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>

                                                                     <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnSearch_Click">Ok</asp:LinkButton>

                                                                </td>
                                                                <td>
                                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                                        <ProgressTemplate>
                                                                            <asp:Label ID="Label3U" runat="server" CssClass="text-danger" Text="Please wait . . . . . . ."></asp:Label>
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlOrder2" runat="server" CssClass="ddlistPull" Width="150px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlOrderad2" runat="server"
                                                                        CssClass="ddlPage62">
                                                                        <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                                        <asp:ListItem Value="desc">Des</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td class="style115">
                                                                    <asp:DropDownList ID="ddlOrder3" runat="server" CssClass="ddlistPull" Width="150px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="style116">
                                                                    <asp:DropDownList ID="ddlOrderad3" runat="server"
                                                                        CssClass="ddlPage62">
                                                                        <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                                        <asp:ListItem Value="desc">Des</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>


                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </div>

                                                <div class="form-group">
                                                    <div class="form-group">
                                            <div class="col-md-9 pading5px asitCol9">
                                                <asp:Label ID="lblPage" runat="server" Text="Page Size:" Visible="False" CssClass="lblName lblTxt"></asp:Label>

                                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False" CssClass="ddlPage"  Width="152px">
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>100</asp:ListItem>
                                                    <asp:ListItem>150</asp:ListItem>
                                                    <asp:ListItem>200</asp:ListItem>
                                                    <asp:ListItem>300</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                                </div>
                                            </div>


                                        </div>
                                    </fieldset>

                                </div>




                    <%-- <asp:Panel ID="Panel4" runat="server">
                                <div class="form-group">
                                    <div class="col-md-11 pading5px asitCol11">

                                        <asp:Label ID="lblSearchlist" runat="server" CssClass="lblTxt lblName" Text="Search List:"></asp:Label>

                                        <asp:DropDownList ID="ddlFieldList1" runat="server" CssClass=" ddlPage" TabIndex="2"></asp:DropDownList>

                                        <asp:DropDownList ID="ddlSrch1" runat="server" CssClass="ddlPage">
                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:TextBox ID="txtSearch1" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                        <asp:DropDownList ID="ddlOperator1" runat="server" CssClass="ddlPage">
                                            <asp:ListItem Value="and">And</asp:ListItem>
                                            <asp:ListItem Value="or">Or</asp:ListItem>
                                        </asp:DropDownList>


                                            <asp:Label ID="lblOrderList" runat="server" CssClass="lblTxt lblName" Text="Order Field:"></asp:Label>

                                            <asp:DropDownList ID="ddlOrder1" runat="server" CssClass=" ddlPage" TabIndex="2"></asp:DropDownList>

                                            <asp:DropDownList ID="ddlOrderad1" runat="server" CssClass="ddlPage">
                                                <asp:ListItem Value="like">Like</asp:ListItem>
                                                <asp:ListItem Value="=">Equal</asp:ListItem>
                                                <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnSearch_Click">Ok</asp:LinkButton>

                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                <ProgressTemplate>
                                                    <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="#000"
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Yellow" Style="text-align: left" Text="Please wait . . . . . . ."
                                                        Width="120px"></asp:Label>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                    </div>
                                </div>
                       
                                        <div class="form-group">
                                        <div class="col-md-9 pading5px asitCol9">

                                            <asp:DropDownList ID="ddlOrder2" runat="server" CssClass="ddlPage" Width="150px"></asp:DropDownList>

                                            <asp:DropDownList ID="ddlOrderad2" runat="server" CssClass="ddlPage" Width="90px">
                                                <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                                <asp:ListItem Value="desc">Descendig</asp:ListItem>
                                            </asp:DropDownList>


                                            <asp:DropDownList ID="ddlOrder3" runat="server" CssClass="ddlPage" Width="150px"></asp:DropDownList>

                                            <asp:DropDownList ID="ddlOrderad3" runat="server" CssClass="ddlPage" Width="90px">
                                                <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                                <asp:ListItem Value="desc">Descendig</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                        <div class="form-group">
                        <div class="col-md-9 pading5px asitCol9">

                            <asp:DropDownList ID="ddlFieldList2" runat="server" CssClass="ddlPage" Width="150px"></asp:DropDownList>

                            <asp:DropDownList ID="ddlSrch2" runat="server" CssClass="ddlPage">
                                <asp:ListItem Value="like">Like</asp:ListItem>
                                <asp:ListItem Value="=">Equal</asp:ListItem>
                                <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                            </asp:DropDownList>


                            <asp:TextBox ID="txtSearch2" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                            <asp:DropDownList ID="ddlOperator2" runat="server" CssClass=" ddlPage">
                                <asp:ListItem Value="and">And</asp:ListItem>
                                <asp:ListItem Value="or">Or</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                                        <div class="form-group">
                                            <div class="col-md-9 pading5px asitCol9">

                                                <asp:DropDownList ID="ddlFieldList3" runat="server" CssClass="ddlPage" Width="150px"></asp:DropDownList>

                                                <asp:DropDownList ID="ddlSrch3" runat="server" CssClass="ddlPage">
                                                    <asp:ListItem Value="like">Like</asp:ListItem>
                                                    <asp:ListItem Value="=">Equal</asp:ListItem>
                                                    <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                    <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                    <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                    <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                </asp:DropDownList>


                                                <asp:TextBox ID="txtSearch3" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-9 pading5px asitCol9">
                                                <asp:Label ID="lblPage" runat="server" Text="Page Size:" Visible="False"></asp:Label>

                                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                                    Width="120px">
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>100</asp:ListItem>
                                                    <asp:ListItem>150</asp:ListItem>
                                                    <asp:ListItem>200</asp:ListItem>
                                                    <asp:ListItem>300</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                            </asp:Panel>--%>
                        </div>
                            </fieldset>

                    </div>
                   

                  

                    <div class="table table-responsive">
                        <asp:Panel ID="Panel10" runat="server" Width="1260px">
                            <asp:GridView ID="gvBillTracking" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="616px"
                                OnRowDataBound="gvBillTracking_RowDataBound" AllowPaging="True"
                                OnPageIndexChanging="gvBillTracking_PageIndexChanging">
                                <Columns>

                                    <asp:TemplateField HeaderText=" Bill No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBillNo" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMRDate" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdate")) %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Bill Ref.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBillRef" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref")) %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" Order No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvOrderNo" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" MRR No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMrrNo" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvProDesc" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="160px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material's Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMDesc" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "matdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvmat" runat="server" Text="Grand Total" Font-Bold="True" HorizontalAlign="Left" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right" Width="200px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSpc" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcdesc")) %>'
                                                Width="85px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnit" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvQty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRate" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamt" runat="server" Font-Size="12px" Height="16px"
                                                Style="text-align: right" Width="60px" ForeColor="#000" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Suplier's Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc9" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Voucher No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvVNum" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Req. No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvReqNo" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MRF No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMrfNo" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>

       
                        <%--<tr>
                                    <td class="style124">
                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="14px"
                                            ForeColor="Yellow"
                                            Style="border-top: 1px solid yellow; border-bottom: 1px solid yellow;"
                                            Text="Field Information:" Width="120px"></asp:Label>
                                    </td>
                                    <td class="style125">
                                        <asp:CheckBox ID="chkallBillTracking" runat="server" AutoPostBack="True"
                                            BackColor="#000066" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            OnCheckedChanged="chkallBillTracking_CheckedChanged" Text="Check All"
                                            Width="80px" />
                                    </td>
                                    <td class="style120">
                                        <asp:Label ID="lblfdate" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="color: #FFFFFF; text-align: right;" Text="From:"
                                            Width="70px"></asp:Label>
                                    </td>
                                    <td class="style122">
                                        <asp:TextBox ID="txtfromdate" runat="server" BorderStyle="None"
                                            Font-Bold="True" Height="16px" Width="87px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat=""></cc1:CalendarExtender>
                                    </td>
                                    <td class="style123">
                                        <asp:Label ID="tDate" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="color: #FFFFFF; text-align: right;" Text="To:"
                                            Width="50px"></asp:Label>
                                    </td>
                                    <td class="style83">
                                        <asp:TextBox ID="txttodate" runat="server" BorderStyle="None" Font-Bold="True"
                                            Height="16px" Width="87px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy " TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>
                                    </td>
                                    <td class="style43">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>--%>
                        <%--<tr>
                                <td class="style83" colspan="6">
                                    <asp:CheckBoxList ID="cblBillTracking" runat="server" BorderColor="#FFCC00"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="2" CellSpacing="0"
                                        CssClass="StyleCheckBoxList" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Height="16px" RepeatColumns="12" Width="1200px"
                                        OnSelectedIndexChanged="cblBillTracking_SelectedIndexChanged"
                                        AutoPostBack="True" RepeatDirection="Horizontal">
                                        <asp:ListItem>aa</asp:ListItem>
                                        <asp:ListItem>bb</asp:ListItem>
                                        <asp:ListItem>cc</asp:ListItem>
                                        <asp:ListItem>dd</asp:ListItem>
                                        <asp:ListItem>ee</asp:ListItem>
                                        <asp:ListItem>ff</asp:ListItem>
                                        <asp:ListItem>gg</asp:ListItem>
                                        <asp:ListItem>hh</asp:ListItem>
                                        <asp:ListItem>mm</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <td class="style43" valign="bottom">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>--%>

            <%--<tr>
                                    <td class="style110">
                                        <asp:Label ID="lblSearchlist" runat="server" Font-Bold="True" Font-Size="14px"
                                            ForeColor="Yellow"
                                            Style="border-top: 1px solid yellow; border-bottom: 1px solid yellow;"
                                            Text="Search List:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style109">
                                        <asp:DropDownList ID="ddlFieldList1" runat="server"
                                            Font-Bold="True" Font-Size="12px" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style111">
                                        <asp:DropDownList ID="ddlSrch1" runat="server" Font-Bold="True"
                                            Font-Size="12px" Width="100px">
                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style112">
                                        <asp:TextBox ID="txtSearch1" runat="server" CssClass="txtboxformat"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style113">
                                        <asp:DropDownList ID="ddlOperator1" runat="server"
                                            Font-Bold="True" Font-Size="12px" Width="60px">
                                            <asp:ListItem Value="and">And</asp:ListItem>
                                            <asp:ListItem Value="or">Or</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style46" rowspan="3">




                                        <asp:Panel ID="Panel5" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="style114">
                                                        <asp:Label ID="lblOrderList" runat="server" Font-Bold="True" Font-Size="14px"
                                                            ForeColor="Yellow"
                                                            Style="border-top: 1px solid yellow; border-bottom: 1px solid yellow;"
                                                            Text="Order Field:" Width="80px"></asp:Label>
                                                    </td>
                                                    <td class="style115">
                                                        <asp:DropDownList ID="ddlOrder1" runat="server" Font-Bold="True"
                                                            Font-Size="12px" Width="150px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style116">
                                                        <asp:DropDownList ID="ddlOrderad1" runat="server"
                                                            Font-Bold="True" Font-Size="12px" Width="90px">
                                                            <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                                            <asp:ListItem Value="desc">Descendig</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style117">
                                                        <asp:LinkButton ID="lbtnSearch" runat="server" BackColor="#003366"
                                                            BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                            Font-Size="12px" Height="16px" OnClick="lbtnSearch_Click"
                                                            Style="text-align: center; color: #FFFFFF; width: 19px;">Ok</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                            <ProgressTemplate>
                                                                <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="#000"
                                                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="Yellow" Style="text-align: left" Text="Please wait . . . . . . ."
                                                                    Width="120px"></asp:Label>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </td>
                                                </tr>





                                                <tr>
                                                    <td class="style114">&nbsp;</td>
                                                    <td class="style115">
                                                        <asp:DropDownList ID="ddlOrder2" runat="server" Font-Bold="True"
                                                            Font-Size="12px" Width="150px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style116">
                                                        <asp:DropDownList ID="ddlOrderad2" runat="server"
                                                            Font-Bold="True" Font-Size="12px" Width="90px">
                                                            <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                                            <asp:ListItem Value="desc">Descendig</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style117">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="style114">&nbsp;</td>
                                                    <td class="style115">
                                                        <asp:DropDownList ID="ddlOrder3" runat="server" Font-Bold="True"
                                                            Font-Size="12px" Width="150px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style116">
                                                        <asp:DropDownList ID="ddlOrderad3" runat="server"
                                                            Font-Bold="True" Font-Size="12px" Width="90px">
                                                            <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                                            <asp:ListItem Value="desc">Descendig</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style117">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>--%>
            <%--<tr>

                                    <td class="style110">&nbsp;</td>
                                    <td class="style109">
                                        <asp:DropDownList ID="ddlFieldList2" runat="server"
                                            Font-Bold="True" Font-Size="12px" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style111">
                                        <asp:DropDownList ID="ddlSrch2" runat="server" Font-Bold="True"
                                            Font-Size="12px" Width="100px">
                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style112">
                                        <asp:TextBox ID="txtSearch2" runat="server" CssClass="txtboxformat"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style113">
                                        <asp:DropDownList ID="ddlOperator2" runat="server"
                                            Font-Bold="True" Font-Size="12px" Width="60px">
                                            <asp:ListItem Value="and">And</asp:ListItem>
                                            <asp:ListItem Value="or">Or</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>--%>
            <%--<tr>
                                    <td class="style110">&nbsp;</td>
                                    <td class="style109">
                                        <asp:DropDownList ID="ddlFieldList3" runat="server"
                                            Font-Bold="True" Font-Size="12px" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style111">
                                        <asp:DropDownList ID="ddlSrch3" runat="server" Font-Bold="True"
                                            Font-Size="12px" Width="100px">
                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style112">
                                        <asp:TextBox ID="txtSearch3" runat="server" CssClass="txtboxformat"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style113">&nbsp;</td>
                                </tr>--%>
            <%--<tr>
                                    <td class="style110">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="color: #FFFFFF; text-align: right;" Text="Page Size:" Visible="False"
                                            Width="70px"></asp:Label>
                                    </td>
                                    <td class="style109">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                            Width="120px">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style111">&nbsp;</td>
                                    <td class="style112">&nbsp;</td>
                                    <td class="style113">&nbsp;</td>
                                </tr>--%>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



