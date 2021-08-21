
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccSalJournal.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccSalJournal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .style18 {
            width: 570px;
        }

        .style175 {
            width: 239px;
        }

        .style176 {
            width: 215px;
        }

        .style178 {
            width: 194px;
        }

        .style190 {
        }

        .style191 {
            width: 101px;
        }

        .style192 {
            width: 230px;
        }

        .style193 {
            width: 236px;
        }

        .style195 {
            height: 20px;
        }

        .style199 {
        }

        .style200 {
            width: 95px;
        }

        .style201 {
            height: 20px;
            width: 17px;
        }

        .style202 {
            width: 17px;
        }

        .style203 {
            width: 13px;
        }

        .style204 {
            width: 8px;
        }

        .style206 {
            height: 20px;
            width: 11px;
        }

        .style207 {
            width: 11px;
        }

        .style208 {
            width: 99px;
        }

        .style209 {
            width: 107px;
        }
    </style>

    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);



        });


        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class=" col-md-4  pading5px asitCol4">

                                            <asp:Label ID="lblDate" CssClass="lblTxt lblName" runat="server" Text="Voucher Date"></asp:Label>

                                            <asp:TextBox ID="txtdate" runat="server" CssClass=" inputtextbox"
                                                TabIndex="1"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                                            
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click"
                                                Text="Ok" TabIndex="2"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class=" col-md-5  pading5px ">

                                            <asp:Label ID="lblcurVounum" CssClass="lblTxt lblName" runat="server" Font-Size="10px" Text="Current Voucher No"></asp:Label>
                                            <asp:LinkButton ID="ibtnvounu" runat="server" CssClass="btn btn-primary srearchBtn" Visible="False" OnClick="ibtnvounu_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            <asp:TextBox ID="txtcurrentvou" runat="server" CssClass=" inputtextbox" AutoPostBack="true" ReadOnly="True"
                                                TabIndex="3"></asp:TextBox>
                                            <asp:TextBox ID="txtCurrntlast6" AutoPostBack="true" runat="server" CssClass=" smltxtBox60px" ReadOnly="True"
                                                TabIndex="4">.</asp:TextBox>
                                        </div>
                                        <div class="col-md-4 pading5px asitCol3">
                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <asp:Panel ID="pnlBill" runat="server" Visible="False">
                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class=" col-md-3  pading5px asitCol3">

                                            <asp:Label ID="lblProject" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                                            <asp:TextBox ID="txtSrchProject" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="imgSearchProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchProject_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                        </div>


                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control inputTxt" TabIndex="13" AutoPostBack="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>


                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="lbtnSelec" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelec_Click">Select</asp:LinkButton>

                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblUnit" runat="server" CssClass="lblTxt lblName">Unit Name</asp:Label>
                                            <asp:TextBox ID="txtSrchUnit" runat="server" CssClass=" inputtextbox" TabIndex="8"></asp:TextBox>

                                            <asp:LinkButton ID="imgSearchUnit" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchUnit_Click" TabIndex="15"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlUnitName" runat="server"
                                                CssClass="form-control inputTxt" TabIndex="13" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                </div>
                            </fieldset>

                            <div class=" table table-responsive">
                                <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Style="text-align: left" Width="685px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid" runat="server"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccCod" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Spcl Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpclCod" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="A/C Description" ItemStyle-Font-Size="9px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccdesc1" runat="server" Font-Size="11px"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")   
                                                                        %>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="11px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFDrAmt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDrAmt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="11px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#000"
                                                HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-Font-Size="11px">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCrAmt" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="11px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#000"
                                                HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                    <%-- <FooterStyle BackColor="#0066CC" />
                                    <HeaderStyle BackColor="#0066CC" BorderStyle="Solid" BorderWidth="2px" 
                                        Font-Bold="True" ForeColor="#000" />
                                    <AlternatingRowStyle BackColor="#FFCCFF" />--%>
                                </asp:GridView>

                            </div>

                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class=" col-md-3  pading5px asitCol3">

                                        <asp:Label ID="lblRefNum" CssClass="lblTxt lblName" Font-Size="11px" runat="server" Text="Ref./Cheq No/Slip No." Width="120px"></asp:Label>
                                        <asp:TextBox ID="txtRefNum" runat="server" CssClass=" inputtextbox" AutoCompleteType="Disabled"></asp:TextBox>

                                    </div>
                                    <div class=" col-md-3 pading5px asitCol3">

                                        <asp:Label ID="lblSrInfo" CssClass="lblTxt lblName" runat="server" Text="Other ref.(if any)" Width="120px"></asp:Label>
                                        <asp:TextBox ID="txtSrinfo" runat="server" CssClass=" inputtextbox" AutoCompleteType="Disabled"></asp:TextBox>

                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFinalUpdate_Click">Final Update</asp:LinkButton>


                                       

                                    </div>

                                    <div class="col-md-1">
                                       
                                    <%--<a class="btn btn-primary primaryBtn"  href='<%=this.ResolveUrl("~/F_22_Sal/LinkMktSalsPayment.aspx?Type=&prjcode=&usircode=")%>' target="_blank" style="margin: 10px 0 0 5px; line-height: 18px;">Details</a>--%>


                                        <asp:HyperLink  ID="lnkDetail" runat="server" CssClass="btn btn-primary" Font-Size="12px"    BorderStyle="None" >Details</asp:HyperLink>

                                    </div>
                                    <div class=" clearfix"></div>
                                </div>
                                <div class="form-group">
                                   
                                    <div class="col-md-8 pading5px">
                                        <asp:Label ID="lblNaration" runat="server"  CssClass="lblTxt lblName" Text="Narration"
                                            Width="120px"></asp:Label>

                                         <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled" CssClass="form-control pull-left"
                                            TextMode="MultiLine" Width="500px"></asp:TextBox>
                                    </div>
                                      <asp:CheckBox ID="chkpost" runat="server" TabIndex="10" Text="post" CssClass="btn btn-primary checkBox" Visible="false" />
                                </div>
                            </div>


                       
                        </div>
                    </asp:Panel>
                </div>
            </div>










            <%--<table style="width: 100%; height: 20px; margin-bottom: 0px;">
                <tr>
                    <td colspan="3" align="center" class="style195">
                        <asp:ImageButton ID="ibtnvounu" runat="server" Height="16px"
                            ImageUrl="~/Image/movie_26.gif" OnClick="ibtnvounu_Click" Width="145px"
                            Visible="False" />
                    </td>
                    <td class="style195"></td>
                    <td class="style206">
                        <asp:Label ID="lblDate" runat="server" CssClass="label2" Text="Voucher Date"
                            Width="100px"></asp:Label>
                    </td>
                    <td class="style201"></td>
                    <td class="style195"></td>
                    <td class="style195">&nbsp;</td>
                </tr>
                <tr>
                    <td class="style200">


                        <asp:Label ID="lblcurVounum" runat="server" CssClass="label2"
                            Text="Current Voucher No." Width="120px"></asp:Label>
                    </td>
                    <td class="style204">
                        <asp:TextBox ID="txtcurrentvou" runat="server" AutoPostBack="True"
                            CssClass="ddl" ReadOnly="True" Width="40px" TabIndex="3"></asp:TextBox>
                    </td>
                    <td align="left" class="style203">
                        <asp:TextBox ID="txtCurrntlast6" runat="server" AutoPostBack="True"
                            CssClass="ddl" ToolTip="You Can Change Voucher Number." Width="40px"
                            ReadOnly="True" TabIndex="4"></asp:TextBox>
                    </td>
                    <td></td>
                    <td class="style207"></td>
                    <td class="style202"></td>
                    <td></td>
                    <td>&nbsp;</td>
                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



