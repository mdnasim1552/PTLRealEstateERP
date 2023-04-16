<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptConTractorBillAll.aspx.cs" Inherits="RealERPWEB.F_09_PImp.RptConTractorBillAll" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);




        });
        function pageLoaded() {

            $('#tblrpcashflow').tblScrollable({

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
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">


                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="Label4" runat="server"
                                            Text="Project Name:"
                                            CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>


                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server"
                                            Width="300px" AutoPostBack="True" CssClass="chzn-select ddlistPull"
                                            OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lbtnOk" runat="server"
                                            OnClick="lbtnOk_Click" CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>


                                    </div>

                                    <div class="col-md-3 pull-right pading5px">
                                        <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-10 asitCol75 pading5px">
                                        <asp:Label ID="Label16" runat="server" Font-Bold="True"
                                            Text="Cont. Name:"
                                            CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtSrcSub" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindSubConName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindSubConName_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        <div class="col-md-3 asitCol3 pading5px">
                                            <asp:DropDownList ID="ddlSubName" runat="server"
                                                Font-Bold="True" CssClass="ddlistPull" Width="300px">
                                            </asp:DropDownList>
                                        </div>



                                        <cc1:ListSearchExtender ID="ddlSubName_ListSearchExtender" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlSubName">
                                        </cc1:ListSearchExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-10 asitCol75 pading5px">
                                        <asp:Label ID="Label2" runat="server" Font-Bold="True"
                                            Text="Subject:" CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtsub" runat="server" CssClass="inputtextbox" Width="400px"></asp:TextBox>



                                        <%--    <asp:Label ID="Label1" runat="server" Font-Bold="True"
                                            Text="Work order Memo No:" CssClass="lblTxt lblName" Width="150"></asp:Label>

                                        <asp:TextBox ID="txtmemo" runat="server" CssClass="inputtextbox" Width="230px"></asp:TextBox>--%>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-md-10 asitCol75 pading5px">

                                        <asp:Label ID="Label5" runat="server" Font-Bold="True"
                                            Text="Work order Memo No:" CssClass="lblTxt lblName" Width="100"></asp:Label>

                                        <asp:TextBox ID="txtmemo" runat="server" CssClass="inputtextbox" Width="400px"></asp:TextBox>
                                    </div>
                                </div>

                            </div>


                            <div class="form-group">

                                <div class="col-md-3 asitCol3 pading5px">
                                    <asp:Label ID="lbldate" runat="server" CssClass=" lblTxt lblName"
                                        Text="Date:"></asp:Label>

                                    <asp:TextBox ID="txtDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>

                                </div>

                                <div class="col-md-4  pading5px">
                                    <asp:Label ID="lblPage" runat="server" CssClass="  smLbl_to" Font-Bold="True"
                                        Text="Size:"></asp:Label>
                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                        CssClass="ddlistPull"
                                        OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                        Width="85px">
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="15">15</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="30">30</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="100">100</asp:ListItem>
                                        <asp:ListItem Value="150">150</asp:ListItem>
                                        <asp:ListItem Value="200">200</asp:ListItem>
                                        <asp:ListItem Value="300">300</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RadioButtonList ID="rbtnconbill" runat="server" CssClass=" rbtnList1 btn btn-primary primaryBtn margin5px"
                                        RepeatColumns="10" RepeatDirection="Horizontal"
                                        Style="width: 200px;">
                                        <asp:ListItem>Details</asp:ListItem>
                                        <asp:ListItem>Top Sheet</asp:ListItem>



                                    </asp:RadioButtonList>

                                    <asp:CheckBox ID="chkfloor" runat="server" Text="Floor Wise" />
                                </div>

                                <div class="col-md-2">
                                    <h5 class="text-center">Last R/A No : <asp:Label runat="server" ID="lbllastra"></asp:Label></h5>
                                </div>

                            </div>
                    </div>
                    </fieldset>
                </div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="viewconbildet" runat="server">
                        <div class="row table-responsive">

                            <asp:Repeater ID="rpconbilldet" runat="server" OnItemDataBound="rpconbilldet_ItemDataBound">
                                <HeaderTemplate>
                                    <table id="tblrpcashflow" class="table-striped table-hover table-bordered grvContentarea">
                                        <tr>
                                            <th>Sl No#</th>

                                            <th style="width: 300px;">Work Description</th>
                                            <th style="width: 50px;">Unit</th>
                                            <th style="width: 70px;">Budget Qty</th>
                                            <th style="width: 70px;">Previous Bill Qty</th>
                                            <th style="width: 70px;">This Bill Qty</th>
                                            <th style="width: 70px;">Total Bill Qty</th>
                                            <th style="width: 70px;">Schedule Rate in TK</th>
                                            <th style="width: 70px;">Amount TK</th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>

                                        <td>
                                            <asp:TextBox ID="txtslno" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "slno"))%>'
                                                Style="text-align: left" Width="50px"></asp:TextBox>
                                        </td>

                                        <td>
                                            <asp:Label ID="lrprsirdesc" runat="server"
                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "fstatus")).Trim()=="1"?

                                                        "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc"))+"</B>"+  "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +

                                                        ((DataBinder.Eval(Container.DataItem, "rsirdesc").ToString().Trim().Length) > 0 ?

                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc")).Trim().Length)==0 ? Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) : "<br>"

                                                            + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) : "")

                                                        :


                                                        "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "flrdesc").ToString().Trim().Length>0 ?
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim().Length>0 ?  "<br>" : "")+
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc")).Trim(): "")
                                                        
                                                        
                                                         %>'
                                                Width="300px">
                                                        
                                                                   
                                         
                                                   <%-- 
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "flrdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc")).Trim(): "") 
                                                                         
                                                                    %>'--%>
                                                 




                                            </asp:Label>

                                        </td>

                                        <td>
                                            <asp:Label ID="lrpunit" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) 
                                                                         
                                                                         
                                                                    %>'
                                                Width="50px"></asp:Label>
                                        </td>

                                        <td style="text-align: right">
                                            <asp:Label ID="lblbudgetqty" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.000;(#,##0.000); ") %>' Width="70px"></asp:Label>
                                        </td>

                                        <td style="text-align: right">
                                            <asp:Label ID="lblrpprebillqty" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbillqty")).ToString("#,##0.000;(#,##0.000); ") %>' Width="70px"></asp:Label>
                                        </td>

                                        <td style="text-align: right">
                                            <asp:Label ID="lblrpcurbillqty" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curbillqty")).ToString("#,##0.000;(#,##0.000); ") %>' Width="70px"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblrptbillqty" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbillqty")).ToString("#,##0.000;(#,##0.000); ") %>' Width="70px"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblrpavgerate" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avgerate")).ToString("#,##0.00;(#,##0.00); ") %>' Width="70px"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:TextBox ID="txtrptbillamt" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbillamt")).ToString("#,##0.000;-#,##0.000; ") %>' Width="70px" Style="text-align: right; border-style: none;"></asp:TextBox>
                                        </td>


                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>


                                        <th>
                                            <asp:LinkButton ID="lbtnUpdate" runat="server" OnClick="lbtnUpdate_Click" CssClass="btn  btn-danger primarygrdBtn" Font-Size="Small"> Update</asp:LinkButton>

                                        </th>
                                        <th></th>
                                        <th></th>
                                        <th></th>


                                    </tr>
                                    </table>
                                </FooterTemplate>


                            </asp:Repeater>


                        </div>


                    </asp:View>

                    <asp:View ID="Viewconbillsum" runat="server">

                        <div class="row table-responsive">

                            <asp:Repeater ID="rpconbilsum" runat="server" OnItemDataBound="rpconbilsum_ItemDataBound">
                                <HeaderTemplate>
                                    <table id="tblrpcashflow" class="table-striped table-hover table-bordered grvContentarea">
                                        <tr>
                                            <th>SL</th>
                                            <th style="width: 250px;">Item Description</th>
                                            <th style="width: 70px;">Paid Amount(Tk.)</th>
                                            <th style="width: 70px;">Total Amount(Tk.)</th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblgvSlNosum" runat="server" Font-Bold="True" Height="16px" Style="text-align: right" Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lrprsirdescsum" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) 
                                                                         
                                                                         
                                                                    %>'
                                                Width="250px"></asp:Label>
                                        </td>



                                        <td style="text-align: right">
                                            <asp:Label ID="lblrppaidamtsum" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="70px"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblrptbillamtsum" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbillamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="70px"></asp:Label>
                                        </td>


                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>



                                    </tr>
                                    </table>
                                </FooterTemplate>


                            </asp:Repeater>


                        </div>

                    </asp:View>


                </asp:MultiView>
            </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
