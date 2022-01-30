<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="linkEmployeeStatus.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_97_MIS.linkEmployeeStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

        };
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
            <div class="card card-fluid container-data mt-5" id='printarea'>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-4 col-sm-6 col-lg-4">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend" id="fmdate" runat="server">
                                    <button class="btn btn-secondary" type="button">From</button>
                                </div>
                                <asp:TextBox ID="txtFdate" runat="server" autocomplete="off" CssClass="from-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtFdate"></cc1:CalendarExtender>
                                <div class="input-group-prepend" id="tdate" runat="server">
                                    <button class="btn btn-secondary" type="button">To</button>
                                </div>
                                <asp:TextBox ID="txtTdate" runat="server" autocomplete="off" CssClass="from-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtTdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTdate"></cc1:CalendarExtender>

                            </div>
                        </div>
                    </div>
                    <div class="row mt-1">

                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Company</button>
                                </div>
                                <asp:DropDownList ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" class="form-control" runat="server" TabIndex="2">
                                </asp:DropDownList>


                            </div>
                        </div>

                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Department</button>
                                </div>
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" TabIndex="7" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>


                            </div>
                        </div>


                    </div>
                    <div class="row mt-1">

                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Section</button>
                                </div>
                                <asp:DropDownList ID="ddlSection" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" class="form-control" runat="server" TabIndex="2">
                                </asp:DropDownList>


                            </div>
                        </div>

                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Page Size</button>
                                </div>
                                 <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">                                            
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>


                            </div>
                        </div>


                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkOk_Click" >OK</asp:LinkButton>
                        </div>
                         
                    </div>
                    <div class="clearfix"></div>

                    <hr />
                    <div class="row">

                        <div class="row table-responsive">
                        <asp:GridView ID="gvDailyLateAttn" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            AllowPaging="True" OnPageIndexChanging="gvDailyLateAttn_PageIndexChanging" CssClass="table-striped table-hover table-bordered grvContentarea"
                           PageSize="100" >
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="Section Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsection" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                            Font-Size="11px" ></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Emp. ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpId" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Font-Size="11px"  ></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpIDCard" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                           Font-Size="11px"  ></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpName" runat="server" Height="16px" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                          Font-Size="11px"   ></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDesig" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                           Font-Size="11px"  ></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Off. Intime">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvoffIntime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "stdtimein")).ToString("hh:mm tt") %>'
                                           Font-Size="11px"  ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Off. Outtime">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvoffouttime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "stdtimeout")).ToString("hh:mm tt") %>'
                                          Font-Size="11px"   ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ac. Intime">
                                    <ItemTemplate>
                                        <asp:label ID="txtgvIntime" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("hh:mm tt") %>'
                                            Font-Size="11px" ></asp:label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderText="Late Time">
                                    <ItemTemplate>
                                      
                                        <asp:Label ID="lblLattime" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "emlatemin")) %>'
                                            Font-Size="11px"  ></asp:Label>
                                 
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
                      
                    </div>
                    </div>
                    <div class="clearfix"></div>
                    <br />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
