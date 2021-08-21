<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptCenterWiseClient.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptCenterWiseClient" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });


        function inputekeyup() {
            var value = $("#myInput").val().trim().toLowerCase();

            $("#<%=gvAdDetails.ClientID %> tr:not(:first)").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });

        }
        function Search_Gridview(strKey) {

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvAdDetails.ClientID %>");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {

                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;

            }
        }
    </script>

    <asp:UpdatePanel ID="uppnlclint" runat="server">
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
            <div class="card card-fluid container-data">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-2">
                            <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>

                            <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="Cal2" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>

                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="Cal3" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-2">
                            <asp:Label ID="lblLTypeName" CssClass="smLbl_to" runat="server" Text="Center Type"></asp:Label>
                            <asp:DropDownList ID="ddlLeadType" runat="server"  OnSelectedIndexChanged="ddlLeadType_SelectedIndexChanged"  AutoPostBack="True" CssClass="form-control">
                            </asp:DropDownList>


                            <asp:TextBox ID="txtmobno" runat="server" CssClass=" inputtextbox" Style="display: none"></asp:TextBox>
                        </div>


                          <div class="col-md-2">
                            <asp:Label ID="Label2" CssClass="smLbl_to" runat="server" Text="Brance Name"></asp:Label>
                            <asp:DropDownList ID="ddlBranchName" runat="server" OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged"  AutoPostBack="True" CssClass="form-control">
                            </asp:DropDownList>


                          
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="lbtnSearch" OnClick="lbtnSearch_Click" runat="server" CssClass="btn btn-success btn-sm" Style="margin-top: 24px;" Text="OK"></asp:LinkButton>

                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label1" runat="server" CssClass="smLbl_to" Text="Search By"></asp:Label>

                            <input type="text" id="myInput" onkeyup="Search_Gridview(this);" placeholder="Search.." title="Type" class="form-control">
                        </div>

                    </div>

                   


                    <div class=" col-md-2  pading5px" style="display: none">

                        <asp:Label ID="lblPage" CssClass="smLbl_to" runat="server" Text="Size"></asp:Label>
                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage">
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

                    </div>


                    <asp:GridView ID="gvAdDetails" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" CssClass="table-striped table-hover table-bordered table-responsive grvContentarea" >
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="S.L">

                                <ItemTemplate>
                                    <asp:Label ID="serialno" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Id">

                                <ItemTemplate>
                                    <asp:Label ID="lblgvusrid" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userid")) %>' Width="55px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Call Centre">

                                <HeaderTemplate>
                                    <asp:TextBox ID="txtgvbranch" BackColor="Transparent" BorderStyle="None" runat="server" Width="140px" placeholder="Call Centre" onkeyup="Search_Gridview(this,2)"></asp:TextBox>


                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblgvbranch" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brname")) %>'
                                        Width="140px" BackColor="Transparent" BorderStyle="None"></asp:Label>


                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />

                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Name">
                                <HeaderTemplate>

                                    <asp:TextBox ID="txtgvName" BackColor="Transparent" BorderStyle="None" runat="server" Width="130px" SortExpression="name" placeholder="Name" onkeyup="Search_Gridview(this,3)"></asp:TextBox>


                                </HeaderTemplate>



                                <ItemTemplate>
                                    <asp:TextBox ID="txtclname" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>'
                                        Width="130px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile">

                                <HeaderTemplate>

                                    <asp:TextBox ID="txtgmobile" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" SortExpression="mob" placeholder="Mobile" onkeyup="Search_Gridview(this,4)"></asp:TextBox>


                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:TextBox ID="txtclmob" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mob")) %>'
                                        Width="70px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">

                                <HeaderTemplate>
                                    <asp:TextBox ID="txtgvemail" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" SortExpression="email" placeholder="Email" onkeyup="Search_Gridview(this,5)"></asp:TextBox>


                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:TextBox ID="txtclemail"
                                        runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                        Width="110px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>

                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>





                            <asp:TemplateField HeaderText="Location">

                                <HeaderTemplate>
                                    <asp:TextBox ID="txtgvlocation" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Location" onkeyup="Search_Gridview(this,6)"></asp:TextBox>


                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lbllocat" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "locat")) %>'
                                        Width="70px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                    <%--<asp:DropDownList ID="ddllocat" runat="server" Width="150" CssClass="form-control inputTxt pull-left">
                                        </asp:DropDownList>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Profession">

                                <HeaderTemplate>
                                    <asp:TextBox ID="txtgvprofession" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Profession" onkeyup="Search_Gridview(this,7)"></asp:TextBox>


                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblpro" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pro")) %>'
                                        Width="60px" BackColor="Transparent" BorderStyle="None"></asp:Label>


                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Lead Source">

                                <HeaderTemplate>
                                    <asp:TextBox ID="txtgvadvertise" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Lead Source" onkeyup="Search_Gridview(this,8)"></asp:TextBox>


                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAdno" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno")) %>' ></asp:Label>
                                    <asp:Label ID="lbladvertise" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "addesc")) %>'
                                        Width="80px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="L.Quality">

                                <HeaderTemplate>
                                    <asp:TextBox ID="txtgvleadq" BackColor="Transparent" BorderStyle="None" runat="server" Width="50px" placeholder="L.Quality" onkeyup="Search_Gridview(this,9)"></asp:TextBox>


                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvleadq" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leaddesc")) %>'
                                        Width="50px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Lead Status">

                                <HeaderTemplate>
                                    <asp:TextBox ID="txtgvleadst" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Lead Status" onkeyup="Search_Gridview(this,10)"></asp:TextBox>


                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvleadst" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leadstatus")) %>'
                                        Width="60px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Interested Project">

                                <HeaderTemplate>
                                    <asp:TextBox ID="txtgvproject" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Interested Project" onkeyup="Search_Gridview(this,11)"></asp:TextBox>


                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblprodid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proid"))%>'></asp:Label>
                                    <asp:Label ID="lblPactCode" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode"))%>'></asp:Label>
                                    <asp:Label ID="lblgvproject" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="100px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />

                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Description">
                                <HeaderTemplate>
                                    <asp:TextBox ID="txtgvfeedback" BackColor="Transparent" BorderStyle="None" runat="server" Width="145px" placeholder="Description" onkeyup="Search_Gridview(this,12)"></asp:TextBox>


                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvinfo"
                                        runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "info")) %>'
                                        Width="80px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Lead Dept."  Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblLeadDept" runat="server" Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")).ToString() %>'
                                        Width="60px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Type" >
                                <ItemTemplate>
                                    <asp:Label ID="lblProdType" runat="server" Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proddesc")).ToString() %>'
                                        Width="60px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />

                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Assign">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvassign" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Assign" onkeyup="Search_Gridview(this,13)"></asp:TextBox>


                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvassign"
                                            runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assignname")) %>'
                                            Width="100px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>--%>












                            <asp:TemplateField HeaderText="Size" Visible="False">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtclsize" runat="server" Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "size")).ToString("#,##0.00;-#,##0.00; ") %>'
                                        Width="60px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Send To" Visible="False">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtclsendto" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sendto")) %>'
                                        Width="130px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />

                            </asp:TemplateField>
                         

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                   <%-- <asp:CheckBox ID="chkassign" runat="server"
                                        Width="20px" />--%>
                                    <asp:CheckBox ID="chkSpec" runat="server"
                                              Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chekstatus"))=="1" %>'
                                              Width="20px" />
                                </ItemTemplate>

                           <FooterTemplate>

                                    <asp:LinkButton ID="lnkbtnAssign" Visible="false" runat="server" OnClientClick="return FunOrdConfirm();"
                                        ToolTip="Assign"> <span class=" fa fa-check "></span> </asp:LinkButton>

                                    <%--<asp:LinkButton ID="lnkbtnMerge"runat="server" OnClick="lnkbtnMerge_Click"><span style="color:red" class="glyphicon  glyphicon-plus-sign"></span>  </asp:LinkButton>--%>
                            </FooterTemplate>





                                <FooterStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>




                            <asp:TemplateField HeaderText="branchcode" Visible="false">

                                <ItemTemplate>
                                    <asp:Label ID="lblgvbranchcode"
                                        runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "branch")) %>'
                                        Width="80px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="left" />
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

            <%--<div id="AddAssign" class="modal animated slideInTop " role="dialog" data-keyboard="false" data-backdrop="static">
                    <div class="modal-dialog">
                        <div class="modal-content  ">
                            <div class="modal-header">

                                <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                                <h4 class="modal-title">
                                    <span class="fa fa-table"></span>Assign Team  </h4>
                            </div>
                            <div class="modal-body form-horizontal">
                                <div class="row-fluid">

                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-3 field-label-responsive">
                                                    <label for="ddlAd">Assign Team </label>
                                                </div>
                                                <div class="col-md-9">


                                                    <asp:DropDownList ID="ddlTeam" runat="server" CssClass="form-control chzn-select ddlclass">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>

                                    </div>




                                </div>


                            </div>
                            <div class="modal-footer ">







                                <asp:LinkButton runat="server" ID="lnkbtnAssignTeam" type="submit" OnClientClick="CloseModalAssign();" CssClass="btn btn-sm btn-success" OnClick="lnkbtnAssignTeam_Click"><i class="fa fa-save" aria-hidden="true"></i> Update</asp:LinkButton>


                            </div>
                        </div>
                    </div>
                </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


