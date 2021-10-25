<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMASTER.Master" AutoEventWireup="true" CodeBehind="EntryFixAssetTransITStock.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.EntryFixAssetITStock" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

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
                <div class="contentPartSmall row">
                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
              
                         <asp:Panel ID="pn1" runat="server">
                                <div class="form-group">
                                    <div class="col-md-8   pading5px  asitCol8">

                                        <asp:Label ID="Label12" runat="server" CssClass=" lblName lblTxt" Text="Trans No:"></asp:Label>

                                        <asp:Label ID="lblCurTransNo1" runat="server" CssClass=" inputtextbox"></asp:Label>
                                        <asp:TextBox ID="txtCurTransNo2" runat="server" CssClass="inputtextbox">00001</asp:TextBox>

                                        <asp:Label ID="Label7" runat="server" CssClass=" smLbl_to" Text="Date:"></asp:Label>
                                        <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server"
                                            Format="dd.MM.yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>
                                         <asp:Label ID="lblrefno" runat="server" CssClass=" lblName lblTxt" Text="Ref No:"></asp:Label>
                                         <asp:TextBox ID="txtRefno" runat="server" CssClass="inputtextbox" Width="100px"></asp:TextBox>


                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3  pading5px">

                                        <asp:Label ID="lblProjectFromList0" runat="server" CssClass=" lblName lblTxt" Text="From Dept.:"></asp:Label>                                    
                                                                  
                                        <asp:DropDownList ID="ddlfrmDept" runat="server"
                                            Width="350px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlprjlistfrom_SelectedIndexChanged" CssClass="chzn-select ddlPage">
                                        </asp:DropDownList>
                                     
                                        </div>
                                     <div class="col-md-1">
                                      
                                               <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                                 <asp:Label ID="lblddlProjectFrom" runat="server" CssClass="inputtextbox" Visible="False" Width="295px"></asp:Label>

                                             </div>
                                        
                                        </div>

                     
<%--                                    </div>
                                </div>--%>

                                <div class="form-group">
                                    <div class="col-md-10  pading5px  asitCol10">

                                        <asp:Label ID="Label13" runat="server" CssClass=" lblName lblTxt" Text="To Detp :"></asp:Label>                                  
                                        <asp:DropDownList ID="ddltoDept" runat="server" OnSelectedIndexChanged="ddltoDept_SelectedIndexChanged"
                                            Width="350px" CssClass="chzn-select ddlPage">
                                        </asp:DropDownList>

                                        <asp:Label ID="lblddlProjectTo" runat="server" CssClass="inputtextbox" Visible="False" Width="295px"></asp:Label>

                                    </div>
                                </div>

                               

                                <div class="form-group">
                                    <div class="col-md-10  pading5px  asitCol10">

                                        <asp:Label ID="lblPreList" runat="server" CssClass=" lblName lblTxt" Text="Prev. Trans List:"></asp:Label>

                                      <%--  <asp:TextBox ID="txtPreTrnsSearch" runat="server" CssClass="inputtextbox"></asp:TextBox>--%>


                                        <asp:LinkButton ID="lbtnPrevVOUList" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnPrevVOUList_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlPrevISSList" runat="server"
                                            Width="320px" CssClass="ddlPage">
                                        </asp:DropDownList>

                                        <asp:Label ID="lblmsg1" runat="server" CssClass=" btn btn-danger primaryBtn"></asp:Label>

                                    </div>
                                </div>
                       
                                </asp:Panel>
                        </div>
                    </fieldset>
                </div>
                <div class="table table-responsive">
                    <asp:Panel ID="pnlgrd" runat="server" Visible="False" Height="300px">
                          <div class="form-group">
                                    <div class="col-md-10  pading5px  asitCol10">

                                        <asp:Label ID="Label2" runat="server" CssClass=" lblName lblTxt" Text="From Employee :"></asp:Label>                                                                         
                                        <asp:DropDownList ID="ddlfrmemployee" runat="server" OnSelectedIndexChanged="ddlfrmemployee_SelectedIndexChanged" AutoPostBack="true"
                                            Width="350px" CssClass="chzn-select ddlPage">
                                        </asp:DropDownList>

                                        <asp:Label ID="Label3" runat="server" CssClass="inputtextbox" Visible="False" Width="295px"></asp:Label>

                                    </div>
                                </div>
                             <div class="form-group">
                                    <div class="col-md-10  pading5px  asitCol10">

                                        <asp:Label ID="Label4" runat="server" CssClass=" lblName lblTxt" Text="To Employee :"></asp:Label>

                                                                            

                                        <asp:DropDownList ID="dlltoemployee" runat="server" 
                                            Width="350px" CssClass="chzn-select ddlPage">
                                        </asp:DropDownList>

                                        <asp:Label ID="Label5" runat="server" CssClass="inputtextbox" Visible="False" Width="295px"></asp:Label>

                                    </div>
                                </div>

                        <div class="form-group">
                            <div class="col-md-10  pading5px  asitCol10">

                                <asp:Label ID="lblResList" runat="server" CssClass=" lblName lblTxt" Text="Resource List:"></asp:Label>
                                <asp:DropDownList ID="ddlreslist" runat="server" Width="300px" CssClass="ddlPage"></asp:DropDownList>
                                <asp:LinkButton ID="lnkselect" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkselect_Click">Select</asp:LinkButton>

                            </div>
                        </div>
                     
                        <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvrescode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Resource Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>

                                         <asp:TemplateField HeaderText="From empId" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblempid" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="From Employee">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfrmempname" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                

                                         <asp:TemplateField HeaderText="To empId" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblempidto" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empidto")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="To Employee">
                                        <ItemTemplate>
                                            <asp:Label ID="lblempname" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empnameto")) %>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bal. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbalqty" runat="server"
                                                Style="font-size: 12px; text-align: center;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="left" />
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="right" Width="60px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkupdate" runat="server"  OnClick="lnkupdate_Click"  CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" HorizontalAlign="right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>
                    </asp:Panel>
                </div>


            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



