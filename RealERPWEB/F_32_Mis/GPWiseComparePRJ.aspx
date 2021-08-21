<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="GPWiseComparePRJ.aspx.cs" Inherits="RealERPWEB.F_32_Mis.GPWiseComparePRJ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $(function () {
                $('[id*=listGroup]').multiselect({
                    includeSelectAllOption: true
                })

                $('[id*=lstProject]').multiselect({
                    includeSelectAllOption: true
                })
            });
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>


    <style>
        .ddmlist .btn {
            padding: 0 12px;
        }

        .ddmlist .btn-group button {
            width: 200px;
        }

        .ddmlist1 .btn-group button {
            width: 300px;
        }

        .ddmlist .multiselect-container {
            width: 100%;
            overflow-y: scroll !important;
            max-height: 300px !important;
        }
    </style>

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
                                    <div class="col-md-4 pading5px ddmlist">
                                        <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text="Group"></asp:Label>

                                        <asp:ListBox ID="listGroup" runat="server" CssClass=" form-control" Width="280px" SelectionMode="Multiple"></asp:ListBox>

                                    </div>

                                    <div class="col-md-4 pading5px ddmlist  ddmlist1">
                                        <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to" Text="Project List"></asp:Label>
                                        <asp:ListBox ID="lstProject" runat="server" CssClass=" form-control" Width="400px" SelectionMode="Multiple" ></asp:ListBox>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:LinkButton ID="lbtnShowData" runat="server" CssClass=" "
                                            OnClick="lbtnShowData_Click">Get Compare</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <div class="table-responsive">

                    <asp:GridView ID="gvCompare" runat="server" OnRowDataBound="gvCompare_RowDataBound" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvgvItmRef" runat="server"  
                                         
                                        Text='<%# DataBinder.Eval(Container.DataItem, "gendesc").ToString() %>'
                                        Width="150px" Font-Size="12px" Style="text-align: left"></asp:Label>
                                </ItemTemplate>
                                
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Resource Description">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvresdesc" runat="server"  
                                         
                                        Text='<%# DataBinder.Eval(Container.DataItem, "resdesc").ToString() %>'
                                        Width="200px" Font-Size="12px" Style="text-align: left"></asp:Label>
                                </ItemTemplate>
                                
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvp1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvp2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvp3" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvp4" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate >
                                    <asp:Label ID="lgvp5" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvp6" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p6")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvp7" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p7")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvp8" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p8")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvp9" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p9")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvp10" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p10")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvp11" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p11")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             

                            <asp:TemplateField HeaderText="Description" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvrescode" runat="server"  
                                         
                                        Text='<%# DataBinder.Eval(Container.DataItem, "rescode").ToString() %>'
                                        Width="200px" Font-Size="12px" Style="text-align: left"></asp:Label>
                                </ItemTemplate>
                                
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                        </Columns>

                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />

                        <EmptyDataTemplate>
                            <div class="btn btn-block btn-default" style="color: red; text-align: center !important; font-style: italic; font-size: 25px;">No Data Found !!! Please Try Agin</div>
                        </EmptyDataTemplate>
                    </asp:GridView>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

