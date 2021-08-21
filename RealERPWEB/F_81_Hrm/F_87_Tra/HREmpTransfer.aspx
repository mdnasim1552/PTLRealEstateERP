<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="HREmpTransfer.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_87_Tra.HREmpTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/KeyPress.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);


            });

            $('.chzn-select').chosen({ search_contains: true });
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:LinkButton ID="lbtnPrevTransList" runat="server" CssClass="lblTxt lblName" OnClick="lbtnPrevTransList_Click">Prev. Trans List:</asp:LinkButton>


                                        <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass="form-control inputTxt" Width="233" TabIndex="2">
                                        </asp:DropDownList>

                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Transfer Date</asp:Label>
                                        <asp:TextBox ID="txtCurTransDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server" Format="dd.MM.yyyy" TargetControlID="txtCurTransDate">
                                        </cc1:CalendarExtender>

                                        <asp:Label ID="trnsferNo" runat="server" CssClass=" smLbl_to">Transfer No</asp:Label>
                                        <asp:Label ID="lblCurTransNo1" runat="server" CssClass=" smLbl_to">Transfer No</asp:Label>
                                        <asp:TextBox ID="txtCurTransNo2" runat="server" ReadOnly="true" CssClass=" inputDateBox "></asp:TextBox>

                                    </div>
                                    <div class="col-sm-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click"
                                            TabIndex="3">Ok</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                           
                        </div>
                    <div class="row">
                         <div class="col-sm-6 ">
                                <asp:Panel ID="pnlCompany" runat="server" CssClass="well padingRight5px" Visible="False">
                                     <div class="form-horizontal">



                                    <div class="form-group">
                                        <asp:Label ID="Label20" runat="server" CssClass="btn btn-success primaryBtn" Text="From Company:"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                            <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control inputTxt pull-left chzn-select " OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">  </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                            <asp:TextBox ID="txtSrchSection" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtnSection" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnSection_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:DropDownList ID="ddlprjlistfrom" runat="server" CssClass="form-control inputTxt pull-left chzn-select" OnSelectedIndexChanged="ddlprjlistfrom_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Employee List:</asp:Label>
                                            <asp:TextBox ID="txtsrchEmp" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnEmpList" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnEmpList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:DropDownList ID="ddlEmpList" runat="server" CssClass="form-control inputTxt pull-left chzn-select" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlEmpList_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12 pading5px">
                                            <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Designation</asp:Label>
                                            <asp:TextBox ID="txtSrcDesignation" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:TextBox ID="txtEmpDesignation" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="120px"></asp:TextBox>

                                        </div>

                                    </div>


                                  </div>
                                </asp:Panel>

                            </div>
                            <div class="col-sm-6 ">
                                <asp:Panel ID="pnlToCompany" runat="server" CssClass="well padingLeft5px" Visible="False">
                                     <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" CssClass="btn btn-success primaryBtn"
                                            Text="To Company:"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                            <asp:TextBox ID="txtSrctoCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtntoCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtntoCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:DropDownList ID="ddlToCompany" runat="server" CssClass="form-control inputTxt pull-left chzn-select" OnSelectedIndexChanged="ddlToCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                            <asp:TextBox ID="txtSrchToSection" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtnToSection" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnToSection_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px asitCol3" style="width:295px;">
                                            <asp:DropDownList ID="ddlprjlistto" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="2">
                                            </asp:DropDownList>                                       
                                            
                                        </div>
                                        <div class="col-sm-1 pading5px">
                                            <asp:LinkButton ID="lnkselect" runat="server" CssClass="btn btn-primary primaryBtn pull-right" OnClick="lnkselect_Click" TabIndex="26">Select</asp:LinkButton>
                                        </div>
                                    </div>


                                    <div class="form-group" style="margin-bottom:8px;">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblResList0" runat="server" CssClass="lblTxt lblName">Present At Place</asp:Label>
                                            <asp:TextBox ID="txtpatplacedate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtpatplacedate_CalendarExtender" runat="server"
                                                Format="dd.MM.yyyy" TargetControlID="txtpatplacedate">
                                            </cc1:CalendarExtender>
                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:RadioButtonList ID="rbtTrnstype" runat="server" CssClass="btn rbtnList1 pading5px chkBoxControl" RepeatColumns="6" RepeatDirection="Horizontal"
                                                TabIndex="28" Width="156px">
                                                <asp:ListItem>Type 1</asp:ListItem>
                                                <asp:ListItem>Type 2</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-md-3 pull-right">
                                            <asp:Label ID="lblmsg" runat="server" Visible="false" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>
                                    </div>

                                         <br />
                                         </div>

                                </asp:Panel>
                            </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                        <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="910px" >
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvidcardno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                            Width="50px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvempname" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="145px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkupdate" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" OnClick="lnkupdate_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdesig" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="100px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="From Company">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtfCompany" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfcomdesc")) %>'
                                            Width="130px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="From Section">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtfprjdesc" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfprjdesc")) %>'
                                            Width="140px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="To Company">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtCompany" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttcomdesc")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="To Section">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvttprjdesc" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttprjdesc")) %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Present At Place">
                                    <ItemTemplate>
                                        <asp:Label ID="txtpatplace" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pplacedate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvremarks" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                            Width="100px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>

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
                    <div class="row">
                        <asp:Panel ID="pnlremarks" runat="server" Visible="False">

                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                                     <asp:Label runat="server" class="lblTxt" id="ContentPlaceHolder1_Label6">Information of Finalcial matters</asp:Label>

                                        </div>
                                        <div class="col-md-6 pading5px">
                                           <asp:TextBox class="form-control" runat="server" tabindex="17" id="txtfmaters" TextMode="MultiLine" cols="20"  rows="2"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                             <asp:Label runat="server" class="lblTxt" id="lbl1">Special Note</asp:Label>

                                        </div>
                                        <div class="col-md-6 pading5px"> 
                                           <asp:TextBox class="form-control" runat="server" tabindex="17" id="txtspnote" TextMode="MultiLine" cols="20"  rows="2"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <%--<table style="width: 100%;">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style31">
                                        <asp:Label ID="Label12" runat="server" CssClass="style16" Font-Bold="True"
                                            Font-Size="12px" ForeColor="White" Style="text-align: right"
                                            Text="Information of Finalcial matters:" Width="130px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfmaters" runat="server" BorderStyle="None"
                                            TextMode="MultiLine" Width="400px" Height="45px" TabIndex="29"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style31">
                                        <asp:Label ID="Label13" runat="server" CssClass="style16" Font-Bold="True"
                                            Font-Size="12px" ForeColor="White" Style="text-align: right"
                                            Text="Special Note:" Width="130px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtspnote" runat="server" BorderStyle="None"
                                            TextMode="MultiLine" Width="400px" Height="45px" TabIndex="30"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>

                            </table>--%>
                        </asp:Panel>
                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


