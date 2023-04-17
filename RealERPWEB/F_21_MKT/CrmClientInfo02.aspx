<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CrmClientInfo02.aspx.cs" Inherits="RealERPWEB.F_21_MKT.CrmClientInfo02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/crm-new-dashboard.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);           

            $(document).on("change", "#DdlDateType", function () {
                // $("#DdlDateType").change(function () {
                var status = this.value;
                // alert(status);
                if (status == "7") {
                    $("#exampleModalSm").modal("toggle");
                }

            });
        });
        function pageLoaded() {
            try {
                let floatingContainer = $(".floating");
                let floatingBtn = $(".floating-btn");
                let floatingHeader = $(".floating-header");

                floatingBtn.click(() => {
                    floatingContainer.addClass("active");
                });
                floatingHeader.click(() => {
                    floatingContainer.removeClass("active");
                });
                //Dashboard Link
                $("#btnDashboard").click(function () {
                    window.location.href = "../F_99_Allinterface/CRMDashboard03?Type=Report";
                });

            }
            catch (e) {

            }
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="wrapper">
                <div class="page mt-4">
                    <div class="page">
                        <div class="row mb-4">
                            <div class="col-md-8">
                                <div class="row align-items-end">
                                    <div class="col-2">
                                        <div class="form-group mb-0">
                                            <label for="form-date-range" class="form-label">
                                                Date</label>

                                             <asp:DropDownList ID="DdlDateType" runat="server" ClientIDMode="Static" class="form-select">
                                                <asp:ListItem Value="0">Today</asp:ListItem>
                                                <asp:ListItem Value="1">Yesterday</asp:ListItem>
                                                <asp:ListItem Value="2">Last 7 Days</asp:ListItem>
                                                <asp:ListItem Value="3">This Month</asp:ListItem>
                                                <asp:ListItem Value="4">Last Month</asp:ListItem>
                                                <asp:ListItem Value="5">This Year</asp:ListItem>
                                                <asp:ListItem Value="6">last Year</asp:ListItem>
                                                <asp:ListItem Value="7">Custom</asp:ListItem>

                                            </asp:DropDownList>
                                            
                                        </div>
                                    </div>
                                    <!-- END -->
                                    <div class="col-3">
                                        <div class="form-group mb-0">
                                             <asp:DropDownList ID="ddlEmpid" data-placeholder="Choose Employee.." runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpid_SelectedIndexChanged">
                                    </asp:DropDownList>
                                            
                                        </div>
                                    </div>
                                    <!-- END -->
                                    <div class="col-3">
                                        <div class="form-group mb-0">
                                            <select class="form-select">
                                                <option>Choose Project</option>
                                            </select>
                                        </div>
                                    </div>
                                    <!-- END -->
                                    <div class="col-4">
                                        <div class="d-flex">
                                            <div class="input-group form-search mb-0 mr-3">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text">
                                                        <i class="fas fa-search"></i>
                                                    </div>
                                                </div>
                                                <input
                                                    type="text"
                                                    class="form-control"
                                                    id="inlineFormInputGroup"
                                                    placeholder="Search Here" />
                                            </div>
                                            <asp:LinkButton ID="lnkOk" runat="server" Text="OK" OnClick="lnkOk_Click" CssClass="mmbd-btn mmbd-btn-primary"></asp:LinkButton>

                                         <%--   <button class="mmbd-btn mmbd-btn-primary">
                                                Apply
                                            </button>--%>
                                        </div>
                                    </div>
                                    <!-- END -->
                                </div>
                            </div>
                            <div class="col-md-4 align-self-end">
                                <div class="d-flex justify-content-end">
                                    <button class="mmbd-btn mmbd-btn-primary mr-2" id="btnaddland" runat="server">
                                        <strong><i class="fas fa-user-plus"></i>&nbsp;Add Lead</strong>
                                    </button>
                                    <button class="mmbd-btn mmbd-btn-primary" id="btnDashboard">
                                        <img
                                            src="../assets/new-ui/images/equalizer.svg"
                                            alt="Dashboard"
                                            class="img-responsive" />
                                        <strong>Dashboard</strong>
                                    </button>
                                </div>
                            </div>
                        </div>
                        <!-- END HEAD -->
                        <div class="mb-4">
                                            <asp:HiddenField ID="lblnewprospect" runat="server" />

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card-flash h-100 px-4 pt-3 pb-4">
                                        <div class="card-body pb-0">
                                            <asp:MultiView ID="MultiView1" runat="server">
                                                <asp:View ID="ViewPersonalInfo" runat="server">
                                                    <div class="row">
                                                    </div>
                                                    <div class="row">
                                                    </div>
                                                    <div class="row">
                                                    </div>
                                                    <div class="row mb-2 btnsavefix">
                                                    </div>
                                                </asp:View>
                                                <asp:View ID="ViewSummary" runat="server">
                                                            <div class="table-responsive">
                                                    <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSummary_RowDataBound"
                                            ShowFooter="false" CssClass="table table-bordered mmbd-table table-striped table-header-flash" AllowPaging="True" OnPageIndexChanging="gvSummary_PageIndexChanging">
                                            <RowStyle />
                                            <Columns>

                                                <%--0--%>
                                                <asp:TemplateField HeaderText="Sl" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "rowid")).ToString("#,##0;(#,##0); ")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <%--1--%>

                                             
                                                <%--4--%>

                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lsircode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'></asp:Label>

                                                        <asp:Label ID="ldesig" runat="server" Width="40px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <%--5--%>
                                                <asp:TemplateField HeaderText="P-ID">
                                                    <ItemTemplate>
                                                           <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                 <asp:Label ID="lsircode1" runat="server" Width="40px" CssClass="text-success"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1")) %>'></asp:Label>
                                                                           
                                                                        </div>
                                                      
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <%--6--%>
                                                <asp:TemplateField HeaderText="Generated Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgenerated" runat="server" Font-Size="11px" Width="70px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reassign Date" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvrassigndate" CssClass="badge badge-light" runat="server" Width="70px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rassigndat"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <%--7--%>

                                                <asp:TemplateField HeaderText="Prospect Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ldesc" runat="server" Width="130px"
                                                            Text='<%# 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")).Trim()
                                                                         
                                                                    %>'>

                                                             


                                                        </asp:Label>



                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                                <%--8--%>
                                                <asp:TemplateField HeaderText="Followup">
                                                    <ItemTemplate>

                                                        <asp:Panel ID="pnlfollowup" runat="server" Width="90px" ClientIDMode="Static">

                                                            <asp:Label ID="lbllfollowuplink" Width="70px" Font-Size="11px" runat="server" ToolTip="Followup" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appbydat")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appbydat")).ToString("dd-MMM-yyyy") %>'>
                                                            </asp:Label>

                                                            <asp:LinkButton ID="lbtnView" ClientIDMode="Static" Style="float: right !important;" Width="15px" ToolTip="View" runat="server" OnClick="lbtnView_Click" CssClass="d-none"><span class="fa  fa-eye"></span></asp:LinkButton>

                                                            <asp:LinkButton ID="lnkEditfollowup" ClientIDMode="Static" Style="float: right !important;" Width="15px" ToolTip="Discoussion" runat="server" OnClick="lnkEditfollowup_Click"><span class="fa fa-edit"></span></asp:LinkButton>



                                                        </asp:Panel>



                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                                <%--9--%>

                                                <asp:TemplateField HeaderText="Lead Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lprefdesc" CssClass="badge badge-light" runat="server" Width="70px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leadsta")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--10--%>

                                                <asp:TemplateField HeaderText="Associate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lassoc" runat="server" Width="90px"
                                                            Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                <%--11--%>

                                                <asp:TemplateField HeaderText="Team Leader" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbusername" runat="server" Width="90px"
                                                            Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                <%--12--%>

                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbllstatus" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--13--%>

                                                <asp:TemplateField HeaderText="Type" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="llTyp" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadType")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%-- <asp:TemplateField HeaderText="Approve Date" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lappdat" runat="server" Width="60px" Font-Size="10px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appbydat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                                <%-- <asp:TemplateField HeaderText="Lead Source" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lLSrc" runat="server" Width="60px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadSrc")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                <%--14--%>
                                                <asp:TemplateField HeaderText="Active" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAct" ClientIDMode="Static" Width="12" ToolTip="" runat="server" OnClick="lnkAct_Click"><span class="fa fa-edit"></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <%--15--%>



                                                <asp:TemplateField HeaderText="Mobile" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvphone" runat="server" Width="80px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--16--%>
                                                <asp:TemplateField HeaderText="Email" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvemail" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--17--%>


                                                <asp:TemplateField HeaderText="Occupation" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvoccupation" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "profession")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--18--%>


                                                <asp:TemplateField HeaderText="Residence" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcaddress" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "caddress")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--19--%>



                                                <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvpactdesc" runat="server" Width="250px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--20--%>


                                                <asp:TemplateField HeaderText="Source">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvLSrc" runat="server" Width="100px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadSrc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "irpersonname"))=="" ?"": "(" + Convert.ToString(DataBinder.Eval(Container.DataItem, "irpersonname")) + ")")
                                                                
                                                            %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--21--%>


                                                <asp:TemplateField HeaderText="Last discussion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldesc" runat="server" Font-Size="12px" Width="300px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ldiscuss")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--22--%>


                                                <asp:TemplateField HeaderText="Notes" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvnotes" runat="server" Width="150px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "virnotes")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--23--%>

                                                <asp:TemplateField HeaderText="Prefered Location" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgprefdesc" runat="server" Width="120px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prefdesc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <%--24--%>

                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvempid" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <%--25--%>

                                                <asp:TemplateField HeaderText="Retreive" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnRetreive" runat="server" Font-Bold="True" Height="12px" ToolTip="Retrieve Prospect" Style="text-align: right" OnClientClick="javascript:return  FunRetProsConfirm()" OnClick="lnkbtnRetreive_Click"><span><i class="fa fa-undo" Style="text-align: center"></i></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <%--26--%>
                                                <asp:TemplateField HeaderText="Next Followup" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbllfollowuplinkkpisum" Width="90px" runat="server"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnfollowupdate")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnfollowupdate")).ToString("dd-MMM-yyyy")%>'>                                                               
                                                        </asp:Label>

                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <%--27--%>
                                                <asp:TemplateField HeaderText="Project Visit<br>Status" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvprojvisit" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projvisit")) %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <%--28--%>
                                                <asp:TemplateField HeaderText="Entry By" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgventryby" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entryby")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
   <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnkgvHeader" runat="server" Font-Bold="True" ToolTip="Edit Header" OnClick="lnkgvHeader_Click"><i class="fa fa-th-large" aria-hidden="true"></i></asp:LinkButton>

                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete"
                                                            runat="server" Font-Bold="True" ToolTip="Delete" Style="text-align: right;" OnClientClick="javascript:return  FunConfirm()" OnClick="lnkDelete_Click">

                                                        <i class=" fa fa-trash"></i></asp:LinkButton>




                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <%--2--%>


                                                <asp:TemplateField HeaderText="Action">

                                                    <ItemTemplate>
                                                       
                                                        <div class="d-flex">
                                                             <asp:LinkButton ID="ViewData" runat="server" CssClass="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1"  ToolTip="View"  OnClick="ViewData_Click"><span><i class="fas fa-eye" aria-hidden="true"></i></span></asp:LinkButton>
                                                                 
                                                             <asp:LinkButton ID="lnkEdit" runat="server" CssClass="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1"  ToolTip="Edit Client Info" Text="Edit" OnClick="lnkEdit_Click"> <span class="fas   fa-edit"></span></asp:LinkButton>
                                                                           
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>


                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <%--3--%>

                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <HeaderTemplate>
                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                            CssClass="btn   btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                       
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />



                                                </asp:TemplateField>
                                            </Columns>
                                            <%--<FooterStyle CssClass="grvFooter" />--%>
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerSettings Mode="NumericFirstLast" />
                                            <%--<PagerStyle CssClass="gvPagination" />--%>
                                            <%--<HeaderStyle CssClass="grvHeader" />--%>
                                        </asp:GridView>
                                                    <asp:GridView ID="gvkpi" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea ">
                                            <RowStyle Height="25px" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNokpi" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgbempid" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>





                                                <asp:TemplateField HeaderText="Employee Name">


                                                    <HeaderTemplate>
                                                        <div class="row">
                                                            <div class="col-md-9">
                                                                <asp:Label ID="lblgvheadername" runat="server">Employee Name</asp:Label>

                                                            </div>


                                                            <div class="col-md-2">
                                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                                    CssClass="btn   btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>

                                                            </div>


                                                        </div>


                                                    </HeaderTemplate>



                                                    <ItemTemplate>
                                                        <asp:Label ID="lowner" runat="server" Width="200px" Font-Size="10px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFtxtTotal" runat="server" Style="text-align: center"
                                                            Width="60px" Text="Total"></asp:Label>
                                                    </FooterTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Call">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblgvkpicall" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpicall_Click"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "call")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFcallsum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Ext. Meeting">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblgvkpiextmeeting" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpiextmeeting_Click"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "extmeeting")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFexmeetingsum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Internal. Meeting">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblgvkpiintmeeting" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpiintmeeting_Click"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "intmeeting")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFintmeetingsum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />

                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="visit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblgvkpivisit" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpivisit_Click"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "visit")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFvisitsum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>






                                                <asp:TemplateField HeaderText="Proposal">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblgvkpiproposal" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpiproposal_Click"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proposal")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFproposalsum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />

                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Leads">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblgvkpileads" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpileads_Click1"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leads")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFleadssum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />

                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Closing">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblgvkpiclosing" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpiclosing_Click"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "close")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFclosingsum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />

                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Others">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblgvkpiothers" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpiothers_Click"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "others")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFotherssum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Total">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblgvkpitotal" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpitotal_Click"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "total")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFtotalsum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="center" VerticalAlign="Middle" Font-Size="12px" />

                                                </asp:TemplateField>






                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>

                                                    <asp:GridView ID="gvkpidet" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvkpidet_RowDataBound"
                                        ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea " PageSize="10">
                                        <RowStyle Font-Size="11px" Height="25px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDeletekpisum" runat="server" Font-Bold="True" Height="16px" ToolTip="Delete" Style="text-align: right" Text="Delete" OnClientClick="javascript:return  FunConfirm()" OnClick="lnkDeletekpisum_Click"><span class=" fa   fa-recycle"></span></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNokpisum" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "rowid")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lsircodekpisum" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lsircode1kpisum" runat="server" Width="50px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Generated">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgeneratedkpisum" runat="server" Width="70px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Followup">
                                                <ItemTemplate>

                                                    <asp:Panel ID="pnlfollowupkpisum" runat="server" Width="110px" ClientIDMode="Static">



                                                        <asp:Label ID="lbllfollowuplinkkpisum" Width="70px" runat="server"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnfollowupdate")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnfollowupdate")).ToString("dd-MMM-yyyy")%>'>
                                                                                             


                                                        </asp:Label>

                                                        <asp:LinkButton ID="lbtnViewkpisum" ClientIDMode="Static" Style="float: right !important;" Width="10px" ToolTip="View" runat="server" OnClick="lbtnViewkpisum_Click"><span class="fa  fa-eye"></span></asp:LinkButton>

                                                        <asp:LinkButton ID="lnkEditfollowupkpisum" ClientIDMode="Static" Style="float: right !important;" Width="10px" ToolTip="Discoussion" runat="server" OnClick="lnkEditfollowupkpisum_Click"><span class="fa fa-edit"></span></asp:LinkButton>



                                                    </asp:Panel>




                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:HyperLink ID="hlbtntbCdataExelkpisum" runat="server"
                                                        CssClass="btn  btn-primary  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>

                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEditkpisum" runat="server" Font-Bold="True" Height="16px" Style="text-align: center" ToolTip="Edit Land & Owner Info" Text="Edit" OnClientClick="javascript:return  FunConfirmEdit()" OnClick="lnkEditkpisum_Click"> <span class=" fa   fa-edit"></span></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />



                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Prospect Details">
                                                <%-- <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchproperty" SortExpression="sirdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Property Details" onkeyup="Search_Gridview(this,7)"></asp:TextBox><br />

                                                </HeaderTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="ldesckpisum" runat="server" Width="220px" Style="font-size: 10px;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")).Trim() %>'>
                                                    </asp:Label>



                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Lead Status">
                                                <%--  <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchstatus" SortExpression="lstatus" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Status" onkeyup="Search_Gridview(this,11)"></asp:TextBox><br />

                                                </HeaderTemplate>--%>

                                                <ItemTemplate>
                                                    <asp:Label ID="lbllstatuskpisum" runat="server" Width="110px" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leadsta"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />

                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="Associate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lassockpisum" runat="server" Width="90px"
                                                        Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                            <%--10--%>

                                            <asp:TemplateField HeaderText="Team Leader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvteamdesckpi" runat="server" Width="90px"
                                                        Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'></asp:Label>
                                                </ItemTemplate>


                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Notes">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvnotes" runat="server" Width="150px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "virnotes")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Prefered Location" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgprefdesc" runat="server" Width="120px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prefdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>













                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvempidkpisum" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))%>'></asp:Label>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                            
                                                        <%--<table
                                                            class="table table-bordered mmbd-table table-striped table-header-flash">
                                                            <thead>
                                                                <tr>
                                                                    <th scope="col" class="text-left">
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <div
                                                                                class="dropdown mmbd-dropdown-icon mr-2">
                                                                                <button
                                                                                    class="btn btn-secondary dropdown-toggle"
                                                                                    type="button"
                                                                                    data-toggle="dropdown"
                                                                                    aria-expanded="false">
                                                                                    <i class="fa fa-filter"></i>
                                                                                </button>
                                                                                <div class="dropdown-menu">
                                                                                    <span class="dropdown-item">Green
                                                                                    </span>
                                                                                    <span class="dropdown-item">Red
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                            <span class="header-label">P-ID </span>
                                                                        </div>
                                                                    </th>
                                                                    <th scope="col" class="text-center">Generated Date
                                                                    </th>
                                                                    <th scope="col" class="text-center">Associate Name
                                                                    </th>
                                                                    <th scope="col" class="text-center">Project Name
                                                                    </th>
                                                                    <th scope="col" class="text-center">Source Name
                                                                    </th>
                                                                    <th scope="col" class="text-center">Followup Date
                                                                    </th>
                                                                    <th scope="col" class="text-center">
                                                                        <div
                                                                            class="d-flex align-items-center justify-content-center">
                                                                            <div
                                                                                class="dropdown mmbd-dropdown-icon mr-2">
                                                                                <button
                                                                                    class="btn btn-secondary dropdown-toggle"
                                                                                    type="button"
                                                                                    data-toggle="dropdown"
                                                                                    aria-expanded="false">
                                                                                    <i class="fa fa-filter"></i>
                                                                                </button>
                                                                                <div class="dropdown-menu">
                                                                                    <span class="dropdown-item">Query
                                                                                    </span>
                                                                                    <span class="dropdown-item">Lead
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                            <span class="header-label">Lead Status
                                                                            </span>
                                                                        </div>
                                                                    </th>
                                                                    <th scope="col" class="text-center">Prospect Name
                                                                    </th>
                                                                    <th scope="col" class="text-center">Last Discussion
                                                                    </th>
                                                                    <th scope="col" class="text-center w-70px">ACTION
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                      <div class="badge badge-light">
                                          RE: Jan 20,2022
                                      </div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                      <div class="badge badge-light">
                                          RE: Jan 20,2022
                                      </div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                      <div class="badge badge-light">
                                          RE: Jan 20,2022
                                      </div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                      <div class="badge badge-light">
                                          RE: Jan 20,2022
                                      </div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>--%>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-4">
                                                            <div class="d-flex align-items-center">
                                                                <div class="length">
                                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm mb-0"
                                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem Selected="true">50</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>150</asp:ListItem>
                                                <asp:ListItem>200</asp:ListItem>
                                                <asp:ListItem>300</asp:ListItem>
                                                <asp:ListItem>600</asp:ListItem>
                                                <asp:ListItem>900</asp:ListItem>
                                                <asp:ListItem>1000</asp:ListItem>
                                                <asp:ListItem>2000</asp:ListItem>
                                                <asp:ListItem>3000</asp:ListItem>
                                                <asp:ListItem>4000</asp:ListItem>
                                                <asp:ListItem>5000</asp:ListItem>
                                                <asp:ListItem>7000</asp:ListItem>
                                                <asp:ListItem>8000</asp:ListItem>
                                                <asp:ListItem>10000</asp:ListItem>
                                            </asp:DropDownList>
                                                                    
                                                                </div>
                                                                <div class="info ml-2">
                                                                    Showing 1 to 19 of 19 records
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-8">
                                                            <%--<nav class="table-pagination">
                                                                <ul class="pagination">
                                                                    <li class="page-item mr-3">
                                                                        <a class="page-link previews" href="#"><i class="fas fa-arrow-left"></i>
                                                                            Previous</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link active" href="#">1</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link" href="#">2</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link" href="#">3</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link">...</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link" href="#">8</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link" href="#">9</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link" href="#">10</a>
                                                                    </li>
                                                                    <li class="page-item ml-3">
                                                                        <a class="page-link next" href="#">Next<i class="fas fa-arrow-right"></i></a>
                                                                    </li>
                                                                </ul>
                                                            </nav>--%>
                                                        </div>
                                                    </div>
                                                </asp:View>
                                            </asp:MultiView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- END -->
                        <div class="floating">
                            <div class="floating-btn">
                                <i class="fas fa-angle-up"></i>
                                <span>To</span>
                                <span>Do</span>
                            </div>
                            <div class="floating-panel">
                                <div class="floating-header">
                                    <div class="floating-title">TO DO</div>
                                    <div class="floating-action">
                                        <i class="fas fa-angle-down down"></i>
                                        <i class="fas fa-angle-up up"></i>
                                    </div>
                                </div>
                                <div class="floating-body">
                                    <ul>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-primary">SW</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#" runat="server">Schedules Work</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lbldws" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-purple">TD</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">To Day Task</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lbltdt" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-info">DWR</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Daily Work Report</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lbldwr" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-dark">KPI</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Key Performance Indicator</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-blue">Call</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Call</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblCall" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-success">Visit</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Visit</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblvisit" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-red">DP</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Day Passed</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblDayPass" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-warning">PME</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Project Meeting External</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblpme" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-info">PMI</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Project Meeting Internal</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblpmi" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-indigo">COM</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Comments</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblComments" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-orange">FRE</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Freezing</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblFreez" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-cyan">DP</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Dead Pros</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblDeadProspect" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-success">Si</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Signed</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblcsigned" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-danger">DB</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Data Bank</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblDatablank" runat="server">0</div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Modal -->


                <div id="exampleModalSm" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
                <!-- .modal-dialog -->
                <div class="modal-dialog modal-sm" role="document">
                    <!-- .modal-content -->
                    <div class="modal-content">
                        <!-- .modal-header -->
                        <div class="modal-header">
                            <h5 class="modal-title">Chose Date Range </h5>
                        </div>
                        <!-- /.modal-header -->
                        <!-- .modal-body -->
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server">From</asp:Label>
                                        <asp:TextBox ID="txtfrmdate" autocomplete="off" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server">To</asp:Label>
                                        <asp:TextBox ID="txttodate" autocomplete="off" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal-body -->
                        <!-- .modal-footer -->
                        <div class="modal-footer">
                            <button type="button" class="btn btn-warning" data-dismiss="modal">Set & Close</button>
                        </div>
                        <!-- /.modal-footer -->
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
