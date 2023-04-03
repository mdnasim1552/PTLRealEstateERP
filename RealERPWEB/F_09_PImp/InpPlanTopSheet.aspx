<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="InpPlanTopSheet.aspx.cs" Inherits="RealERPWEB.F_23_CR.InpPlanTopSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function Search_Gridview(strKey, cellNr) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvMatWise.ClientID %>");
            var rowData;
            for (var i = 0; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].cells[cellNr].innerHTML;
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

        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            try {

                $("input, select")
                    .bind("keydown",
                        function (event) {
                            var k1 = new KeyPress();
                            k1.textBoxHandler(event);
                        });
          
                $('.chzn-select').chosen({ search_contains: true });
                $('#<%=this.gvMatWise.ClientID%>').tblScrollable();

            }
            catch (e) {
                alert(e.mesage);
            }
        };

        function FunMoneyReceipt(url) {
            window.open('' + url + '', '_blank');
        }

    </script>


    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
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
            <div class=" card card-fluid ">
                <div class=" card-body">
                    <div class="row mt-4">
                        <div class="col-md-1">
                            <asp:Label ID="lblDate" runat="server" CssClass="control-label" Text="From"></asp:Label>
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm" ToolTip="dd-MMM-yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="To"></asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" ToolTip="dd-MMM-yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender1" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>
                        
                        <div class="col-md-3 col-sm-3 col-lg-3" style="margin-top: 22px;">
                            <asp:RadioButtonList ID="rbtnList1" runat="server" CssClass="form-control"
                                RepeatColumns="7" RepeatDirection="Horizontal">
                                <asp:ListItem>Work Wise</asp:ListItem>
                                <asp:ListItem>Materials Wise</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-1" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: 22px;">
                            <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HpblnkNew" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_09_PImp/ImplementPlanWithMaterials">Monthly Implementation Plan with Materials</asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class=" card card-fluid " style="min-height: 600px">
                <div class=" card-body">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewWorkWise" runat="server">
                            <div class="row">

                                <asp:GridView ID="gvWrkWise" runat="server" AutoGenerateColumns="False"
                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvWrkWise_RowDataBound"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvINSlNo0" runat="server" Font-Bold="True"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)%>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchproj" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Project Name" onkeyup="Search_Gridview(this,1)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPactdesc" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plan No">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchvouno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Plan No" onkeyup="Search_Gridview(this,2)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvvouno" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vouno")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plan Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvimpdate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "impdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>

                                            </ItemTemplate>
                                              <FooterTemplate>
                                                <asp:Label ID="lblgvFTotal" runat="server" Style="text-align: right" Width="75px">Total</asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Plan Amount">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvwrkamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFwrkamt" runat="server" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Materials Amount">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmatamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFmatamt" runat="server" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkMoneyRcptEdit" ToolTip="Edit" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class=" fa fa-edit"></span>
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="pactcode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpactcode" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <RowStyle CssClass="grvRows" />
                                </asp:GridView>

                            </div>
                        </asp:View>
                        <asp:View ID="ViewMatWise" runat="server">
                            <div class="row">

                                <asp:GridView ID="gvMatWise" runat="server" AutoGenerateColumns="False"
                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvWrkWise_RowDataBound"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvINSlNo0" runat="server" Font-Bold="True"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)%>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchproj" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Project Name" onkeyup="Search_Gridview(this,1)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPactdesc" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchimpdate" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Month" onkeyup="Search_Gridview(this,2)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvimpdate" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "impdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchsirdesc1" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Materials Group" onkeyup="Search_Gridview(this,3)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrsirdesc1" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchrsirdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Materials Name" onkeyup="Search_Gridview(this,4)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrsirdesc" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFTotal" runat="server" Style="text-align: right" Width="75px">Total</asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qty">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>' Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrate" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>' Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Materials Amount">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmatamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFmatamt" runat="server" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Site Supply</br> Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsitesupdate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sitesupdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>

                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="pactcode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpactcode" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <RowStyle CssClass="grvRows" />
                                </asp:GridView>

                            </div>
                        </asp:View>

                    </asp:MultiView>


                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
