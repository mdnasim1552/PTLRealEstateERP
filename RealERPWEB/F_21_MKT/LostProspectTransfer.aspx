<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="LostProspectTransfer.aspx.cs" Inherits="RealERPWEB.F_21_MKT.LostProspectTransfer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .mt20 {
            margin-top: 20px;
        }

        .panel {
            margin-bottom: 20px;
            background-color: #fff;
            border: 1px solid transparent;
            border-radius: 4px;
            -webkit-box-shadow: 0 1px 1px rgba(0,0,0,.05);
            box-shadow: 0 1px 1px rgba(0,0,0,.05);
        }

       /* .chzn-container-single .chzn-single 
        {
        
            height: 30px !important;
            line-height: 35px;
        }*/

    </style>

    <script type="text/javascript">
        $(document).ready(function () {
           // $('.chzn-select').chosen({ search_contains: true });
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true }

        });
        function pageLoaded() {
          


            $('.chzn-select').chosen({ search_contains: true });
        };

        function openModaldis() {

            $('#mdiscussion').modal('toggle');
            //  $('#lbtntfollowup').click();
        }
        function CloseModaldis() {

            $('#mdiscussion').modal('toggle');
        }

    </script>


    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
            <div class="card card-fluid  mb-1">
                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-3 col-md-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server">Dristribute To</asp:Label>
                                <asp:DropDownList ID="ddlEmpNameTo" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>


                          <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                
                                
                                  <asp:Label ID="lbldate" runat="server">Assign Date</asp:Label>
                                  <asp:TextBox ID="txtDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtDate" Enabled="true" ></cc1:CalendarExtender>
                                
                            </div>
                        </div>
                       
                        <div class="col-sm-2  col-md-2  col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="search" runat="server">Search</asp:Label>
                                <asp:TextBox ID="txtVal" runat="server" CssClass="form-control form-control-sm" TextMode="Search"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <asp:LinkButton ID="SrchBtn" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="SrchBtn_Click">Search</asp:LinkButton>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>500</asp:ListItem>
                                    <asp:ListItem>800</asp:ListItem>
                                    <asp:ListItem>1000</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                    <asp:ListItem>3000</asp:ListItem>
                                    <asp:ListItem>5000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lbl" runat="server" Visible="false">Dristribute To</asp:Label>
                                <asp:DropDownList ID="ddlEmpid" ClientIDMode="Static" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpid_SelectedIndexChanged" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid container-data">
                <div class="card-body mb-0 pb-0">
                    <div class="row mb-0 pb-0">
                        <div class="col-md-8">
                            <div class="row">
                                <asp:GridView ID="gvProspectWorking" runat="server" AutoGenerateColumns="False"
                                    PageSize="200" AllowPaging="true" OnPageIndexChanging="gvProspectWorking_PageIndexChanging"
                                    ShowFooter="True" CssClass=" table-striped table-bordered grvContentarea"
                                    HeaderStyle-Font-Size="14px" Width="800px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" 
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Prospect Code" HeaderStyle-Width="30px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvproscod1" runat="server" 
                                                    Style="text-align: center"
                                                    Text='<%# "P-" + Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod1")) %>' Width="100px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField
                                            HeaderText="Prospect Name">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblProsName" runat="server" Font-Bold="True"
                                                    Text="Prospect Name" Width="110px"></asp:Label>
                                                <asp:HyperLink ID="hlnkbtnProsWorking" runat="server"
                                                    CssClass="btn  btn-success  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEditfollowup" ClientIDMode="Static" ToolTip="Discoussion" runat="server" OnClick="lnkEditfollowup_Click">
                                                    <asp:Label ID="lblgvProsName" runat="server"
                                                        Font-Underline="False"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prospectname")) %>'
                                                        Width="180px"></asp:Label>
                                                </asp:LinkButton>
                                                <asp:Label ID="lblteamcode" runat="server" Visible="false"
                                                   
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamcode")) %>'></asp:Label>
                                                <asp:Label ID="lblproscod" runat="server" Visible="false"
                                                   
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod")) %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Associate Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvassocname" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assocname")) %>'
                                                    Width="250px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Contact No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPhone" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                    Width="90px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Profession">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProfession" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "profession")) %>'
                                                    Width="100px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAddress" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress")) %>'
                                                    Width="140px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Interested Project">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIntProject" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "interestproj")) %>'
                                                    Width="160px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lost Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIntlostdate" runat="server" 
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lostdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="110px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAllfrm" OnCheckedChanged="chkAllfrm_CheckedChanged" runat="server" AutoPostBack="True" Text="All" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chckTrnsfer" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkper"))=="True" %>'
                                                    Width="50px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPaginationNew" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="mdiscussion" class="modal fade   animated slideInTop " role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-full-width  ">
            <div class="modal-content modal-content-full-width">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <i class="fa fa-hand-point-right"></i>
                        Discussion </h4>
                    <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body ">
                    <div class="col-md-12 col-lg-12">
                        <asp:Repeater ID="rpclientinfo" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="col-md-12  col-lg-12">
                                    <div class="well">
                                        <div class="col-sm-12 panel">
                                            <div class=" col-sm-12">
                                                <p>
                                                    <strong><%# DataBinder.Eval(Container, "DataItem.prosdesc")%></strong> <%# DataBinder.Eval(Container, "DataItem.kpigrpdesc").ToString() %>  on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %><br>
                                                    <strong>Participants:</strong> <%# DataBinder.Eval(Container, "DataItem.partcilist").ToString() %><br>
                                                    <strong>Summary:</strong><span class="textwrap"><%# DataBinder.Eval(Container, "DataItem.discus").ToString() %></span><br>
                                                    <strong>Next Action:</strong> <%# DataBinder.Eval(Container, "DataItem.nfollowup").ToString() %> on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy hh:mm tt")%><br>
                                                    <strong>Comments:</strong> <%# DataBinder.Eval(Container, "DataItem.disgnote").ToString() %>
                                                    <br>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
