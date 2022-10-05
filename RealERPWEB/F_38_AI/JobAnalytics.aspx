<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="JobAnalytics.aspx.cs" Inherits="RealERPWEB.F_38_AI.JobAnalytics" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


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

            <div class="card mt-5">
                <div class="row">
                    <div class="col-lg-6" style=" width: 100%">
                        <%--<h2>Demo Projects</h2>
                        <div class="card">
                            <div class="card-header bg-info"></div>
                            <div class="card-body">
                            

                            </div>
                        </div>--%>
                         <div class="card">
                            <div class="card-header bg-light"><span class="font-weight-bold text-muted">Project Information</span></div>
                            <div class="card-body" runat="server" id="engst">
                                <table class="table table-striped table-hober tblEMPinfo mt-2">
                    <%--                <thead>
                                        <tr>
                                            <th></th>
                                            <th></th>

                                        </tr>
                                    </thead>--%>
                                    <tbody class="">
                                        <tr>
                                            <td class="font-weight-bold">Project Name</td>
                                            <td>
                                                <asp:Label ID="lblprjname" runat="server" ></asp:Label>
                                            </td>

                                        </tr>
                                        <tr>
                                           <td class="font-weight-bold ">Project Type</td>
                                            <td>
                                                <asp:Label ID="lblprjtype" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="font-weight-bold">Work Type</td>
                                            <td>
                                                <asp:Label ID="lblwktype" runat="server"></asp:Label>
                                            </td>
                                        </tr>


                                        <tr>
                                           <td class="font-weight-bold">Create Date</td>
                                            <td>
                                                <asp:Label ID="lblcreatedat" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="font-weight-bold">Quantity</td>
                                            <td>
                                                <asp:Label ID="lblqty" runat="server"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                           <td class="font-weight-bold">Customer Name</td>
                                            <td>
                                                <asp:Label ID="lblcusname" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td class="font-weight-bold">Present Salary</td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server">0000</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="font-weight-bold">Job Seperation Type</td>
                                            <td>
                                                <asp:Label ID="lblseptype" runat="server"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                           <td class="font-weight-bold">Last Date of Office</td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                           <td class="font-weight-bold">Service Period</td>
                                            <td>
                                                <asp:Label ID="lblservlen" runat="server"></asp:Label>
                                            </td>
                                        </tr>--%>
                                    </tbody>
                                </table>
                         
                        
        
                            </div>
                        </div>
                    </div>
                          <div class="col-lg-6 text-center" style="width: 100%">
                       <img id="ContentPlaceHolder1_Image1" src="https://cdn.pixabay.com/photo/2016/09/03/14/35/algorithms-1641861_960_720.png" class="mt-5" style="height: 100px; width: 100px;">
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-md-2">
                        <div class="shadow-lg p-3 mb-5 bg-body rounded">
                            <h2 class="text-center" runat="server" id="doninstnace">25K</h2>
                            <div class="text-center"><br />
                                <p> Total Number of Complate class insatance</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="shadow-lg p-3 mb-5 bg-body rounded">
                            <h2 class="text-center text-primary" runat="server" id="attinstance">0</h2>
                            <div class="text-center"><br />
                                <p> Number of Complete attribute insatance</p>

                            </div>
                        </div>
                    </div>
                <%--doninstnace=sum(doninstnace),attinstance=sum(attinstance),qaspent=sum(qaspent), annotspent=sum(annotspent), adminspnt=sum(adminspnt), ttlskip--%>

                    <div class="col-md-2">
                        <div class="shadow-lg p-3 mb-5 bg-body rounded">
                            <h2 class="text-center" runat="server" id="qaspent">0.1<small>hrs</small></h2>
                            <div class="text-center">
                                <p>Out of 770.4hrs  <br /> <b class="text-primary">QA hours spent</b> </p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="shadow-lg p-3 mb-5 bg-body rounded">
                            <h2 class="text-center text-primary" runat="server" id="annotspent">410.8<small>hrs</small></h2>
                            <div class="text-center">
                                <p>Out of 770.4hrs <br /> <b class="text-primary">Annot, hours spent</b> </p>
                            </div>
                        </div>
                    </div>
                      <div class="col-md-2">
                        <div class="shadow-lg p-3 mb-5 bg-body rounded">
                            <h2 class="text-center text-primary" runat="server" id="adminspnt">359.4<small>hrs</small></h2>
                            <div class="text-center">
                                <p>Out of 770.4hrs  <br />  <b class="text-primary">Admin hours spent</b> </p>
                            </div>
                        </div>
                    </div>
                      <div class="col-md-2">
                        <div class="shadow-lg p-3 mb-5 bg-body rounded">
                            <h2 class="text-center text-primary" runat="server" id="ttlskip">0.00</h2>
                            <div class="text-center">
                                <p>Out of 770.4hrs  <br />  <b class="text-primary">Total Number of Skip</b> </p>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <h3>Users</h3>
                </div>
                <div class="row">
                    <asp:GridView ID="gv_UserAnalytic" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" Width="100%">
                        <Columns>

                            <asp:TemplateField HeaderText="SL # ">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right; font-size: 12px;"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                        ForeColor="Black"></asp:Label>

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User">
                                <ItemTemplate>
                                    <asp:Label ID="lbluser" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "")) %>'
                                        Width="500px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role">
                                <ItemTemplate>
                                    <asp:Label ID="lblbatchid" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Work Hours">
                                <ItemTemplate>
                                    <asp:Label ID="lblworkour" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                          
                        </Columns>
                        <PagerStyle CssClass="gvPagination" />

                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
