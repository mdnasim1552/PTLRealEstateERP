<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptNotification.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptNotification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card card-fluid">
        <div class="card-body" style="min-height: 600px;">
            <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="False"
                ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea">
                <RowStyle />
                <Columns>

                    <asp:TemplateField HeaderText="Sl">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>







                    <asp:TemplateField HeaderText="Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lsircode" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'></asp:Label>

                            <asp:Label ID="ldesig" runat="server" Width="40px" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="P-ID">
                        <ItemTemplate>
                            <asp:Label ID="lsircode1" runat="server" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1")) %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Generated">
                        <ItemTemplate>
                            <asp:Label ID="lblgenerated" runat="server" Font-Size="10px"
                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>




                    <asp:TemplateField HeaderText="Client Details">
                        <ItemTemplate>
                            <asp:Label ID="ldesc" runat="server" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Associate">
                        <ItemTemplate>
                            <asp:Label ID="lassoc" runat="server" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                        </ItemTemplate>


                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Team Head">
                        <ItemTemplate>
                            <asp:Label ID="lblbusername" runat="server" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'></asp:Label>
                        </ItemTemplate>


                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lbllstatus" runat="server" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate>
                            <asp:Label ID="llTyp" runat="server" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadType")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Approve Date" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lappdat" runat="server" Width="60px" Font-Size="10px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appbydat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                    <%--  <asp:TemplateField HeaderText="Preferred Location" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lprefdesc" runat="server" Width="60px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prefdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                    <%-- <asp:TemplateField HeaderText="Lead Source" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lLSrc" runat="server" Width="60px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadSrc")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Active">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkAct" ClientIDMode="Static" Width="10" ToolTip="" runat="server"><span class="fa fa-edit"></span></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Mobile">
                        <ItemTemplate>
                            <asp:Label ID="lblgvphone" runat="server" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="lblgvemail" runat="server" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Occupation">
                        <ItemTemplate>
                            <asp:Label ID="lblgvoccupation" runat="server" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "profession")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Residence">
                        <ItemTemplate>
                            <asp:Label ID="lblgvcaddress" runat="server" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "caddress")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Interested Project">
                        <ItemTemplate>
                            <asp:Label ID="lblgvpactdesc" runat="server" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Source">
                        <ItemTemplate>
                            <asp:Label ID="lblgvLSrc" runat="server" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadSrc")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Feedback">
                        <ItemTemplate>
                            <asp:Label ID="lblgvfeedback" runat="server" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ldiscuss")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Comments">
                        <ItemTemplate>
                            <asp:Label ID="lblComments" runat="server" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "note")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkdiss" ClientIDMode="Static" ToolTip="Add Discussion" runat="server">
                                                    <img src="../Image/meeting.svg" width="20" height="20" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="left" />
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
</asp:Content>

